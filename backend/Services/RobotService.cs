using System;
using System.Linq;
using backend.Data;
using backend.Models;
using System.Net.Http;
using System.Threading;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Services
{
    public class RobotService : IHostedService, IDisposable
    {
        private Timer _timer;
        private IServiceProvider Services { get; set; }
        private readonly ILogger<RobotService> _logger;
        private readonly HttpClient _client;

        private Task<List<Robot>> Hosts { get; set; }

        public RobotService(
            ILogger<RobotService> logger,
            IServiceProvider services,
            IHttpClientFactory clientFactory)
        {
            _logger = logger;
            Services = services;
            _client = clientFactory.CreateClient();
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
                await LoadStatus(host, db);
            }
        }

        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = false,
            IgnoreNullValues = true,
            WriteIndented = true,
        };

        private async Task LoadMissions(Robot host, ApplicationDbContext db)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                var response = await _client.SendAsync(HttpRequestMessage(host, "/missions"));

                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();

                    var missions =
                        await JsonSerializer.DeserializeAsync<List<Mission>>(responseStream, _options);
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
                SetRobotOffline(host, db, e);
            }
        }

        private async Task LoadStatus(Robot host, ApplicationDbContext db)
        {
            // No need to send request when robot is offline
            if (!host.IsOnline) await Task.CompletedTask;

            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                var response = await _client.SendAsync(HttpRequestMessage(host, "/status"));

                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    var status = await JsonSerializer.DeserializeAsync<Status>(responseStream, _options);

                    status.RobotId = host.Id;
                    var isAvalible = db.Statuses.Any(s => s.SerialNumber.Equals(status.SerialNumber));
                    if (isAvalible)
                    {
                        var currentStatus = db.Statuses.First(s => s.SerialNumber.Equals(status.SerialNumber));

                        status.Id = currentStatus.Id;
                        host.Hostname = status.RobotName;
                        db.Statuses.Update(status);
                        db.SaveChanges();
                        return;
                    }

                    db.Statuses.Add(status);

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //SetRobotOffline(host, db, e);
                  _logger.LogCritical("The Robot may be is offline");
            }
        }

        private void SetRobotOffline(Robot host, ApplicationDbContext db, Exception e)
        {
            var robot = db.Robots.Find(host.Id);
            robot.IsOnline = false;
            db.Update(robot);
            db.SaveChanges();
            _logger.LogCritical("The Robot may be is offline: " + host.BasePath);
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
