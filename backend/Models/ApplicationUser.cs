using Microsoft.AspNetCore.Identity;

namespace backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] BPasswordHash { get; set; }
        public byte[] BPasswordSalt { get; set; }
    }
}