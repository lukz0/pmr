using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Serialization;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()=> await _context.Statuses.Include(r => r.Velocity)
                .Include(r => r.Robot)
                .Include(p => p.Position).ToListAsync();

        // GET: api/Status/5
        [HttpGet("robotid={id}")]
        public List<Status> GetStatusByRobot(int id)
        {
            var status = _context.Statuses.Where(r => r.RobotId == id).ToList();
            return status;
        }
        
        // PUT: api/Status/5
        [HttpPut("robotid={id}")]
        public async Task<IActionResult> PutStatusByRobot(int id, PutStatus putStatus)
        {
            var responseBuilder = new StringBuilder();
            var httpClient = new HttpClient();
            var content = new StringContent($"{{\"state_id\": {putStatus.StateId}}}", Encoding.UTF8,
                "application/json");

            foreach (Robot robot in await _context.Robots.Where(r => r.Id == id).ToListAsync())
            {
                if (robot.IsOnline)
                {
                    try
                    {
                        httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Basic", robot.Token);
                        httpClient.PostAsync($"{robot.Hostname}/status", content)
                            .ContinueWith(async res => (await res).Dispose());
                    }
                    catch (Exception e)
                    {
                        string eStr = e.ToString();
                        responseBuilder.AppendLine(eStr);
                        await Console.Error.WriteAsync(eStr);
                    }
                }
            }
            
            httpClient.Dispose();
            content.Dispose();
            string response = responseBuilder.ToString();
            if (response.Length == 0)
            {
                return StatusCode(500, response);
            }
            return NoContent();
        }
        
        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        // PUT: api/Status/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Status
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetStatus", new {id = status.Id}, status);
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Status>> DeleteStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return status;
        }

        private bool StatusExists(int id) => _context.Statuses.Any(e => e.Id == id);
    }
}