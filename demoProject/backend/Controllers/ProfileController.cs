using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.ProfileLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileAdmin _profileAdmin;
        
        public ProfileController(ProfileAdmin profileAdmin)
        { 
            _profileAdmin = profileAdmin;
        }
        
        
        [HttpGet]
        public List<Profile> GetProfiles()
        {
            return _profileAdmin.GetProfiles();
        }
        
        
        [HttpGet("{name}")]
        public IActionResult GetProfile(string name)
        {
            var profile = _profileAdmin.GetProfile(name);
            if (profile == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(profile);
            }
        }
        
        [HttpPost]
        public Profile CreateProfile(Profile profile)
        {
            _profileAdmin.AddProfile(profile);
            return profile;
        }
        
        
        [HttpPut]
        public IActionResult UpdateProfile(Profile profile)
        {
            _profileAdmin.UpdateProfile(profile);
            return Ok();
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteProfile(int id)
        {
            _profileAdmin.DeleteProfile(id);
            return Ok();
        }
    }
}