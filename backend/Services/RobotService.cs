using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace backend.Services
{
    public class RobotService : IHostedService, IDisposable
    {
        private Timer _timer;
        private IServiceProvider Services { get; set; }
        private readonly ILogger<RobotService> _logger;
        private readonly IHttpClientFactory _clientFactory;

        private Task<List<Robot>> Hosts { get; set; }
        
        public RobotService(
            ILogger<RobotService> logger,
            IServiceProvider services,
            IHttpClientFactory clientFactory)
        {
            _logger = logger;
            Services = services;
            _clientFactory = clientFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(BackgroundWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void LoadData()
        {
            using var serviceScope = Services.CreateScope();
            var db = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            Hosts = db.Robots.ToListAsync();
        }

        private async void BackgroundWork(object state)
        {
            LoadData();
            var db = Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            foreach (var host in Hosts.Result)
            {
                await LoadMissions(host, db);
                await LoadStatus(host);
            }
        }
        
        private async Task LoadMissions(Robot host, ApplicationDbContext db)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            try
            {
                var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Accept.Clear();
                var response = await client.SendAsync(HttpRequestMessage(host, "/missions"));
                
                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();

                    var missions =
                        await JsonSerializer.DeserializeAsync<List<Mission>>(responseStream, jsonSerializerOptions);
                    foreach (var mission in missions)
                    {
                        mission.RobotId = host.Id;
                        if (!db.Missions.Any(m => m.Name.Contains(mission.Name) 
                                                  && m.RobotId.Equals(mission.RobotId)))
                            db.Missions.Add(mission);
                    }

                    host.IsOnline = true;
                    db.Robots.Update(host);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                
                var robot = db.Robots.Find(host.Id);
                robot.IsOnline = false;
                db.Update(robot);
                db.SaveChanges();
                _logger.LogCritical("The Robot may be is offline: "+ e.Message);
            }
        }

        private async Task LoadStatus(Robot host)
        {
            if (!host.IsOnline) await Task.CompletedTask;
            
            
        }

        private static HttpRequestMessage HttpRequestMessage(Robot host, string path)
        {
            // This method will be injected by the caller with appropriated data ex: requestUrl and authorization
            var request = new HttpRequestMessage(HttpMethod.Get, host.BasePath + path);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization",
                $"Basic {host.Token}");
            return request;
        }


        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}