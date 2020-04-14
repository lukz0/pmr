using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using backend.Models.Robot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MissionsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private ILogger _logger;
        
        public IEnumerable<Mission> Missionses { get; private set; }

        public bool GetMissionsError { get; set; }

        public MissionsController(IHttpClientFactory clientFactory, ILogger<MissionsController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }
        
        [HttpGet("missions")]
        public async Task GetRobot()
        {
            // This method will be injected by the caller with appropriated data ex: requestUrl and authorization
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5050/api/missions/");
            request.Headers.Add("Accept", "application/json");
            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    Missionses = await JsonSerializer.DeserializeAsync<IEnumerable<Mission>>(responseStream);
                }
                else
                {
                    GetMissionsError = true;
                    Missionses = ArraySegment<Mission>.Empty;
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical("Make sure the FakeAPI is running. Messege: "+ e.Message);
                throw;
            }
            
        }
    }
}