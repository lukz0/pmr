using backend.Models;
using backend.Models.Robots;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<Mission> Missions { get; set; }
        
        // From fronted to robot
        public DbSet<MissionQueueRequest> MissionQueueRequests { get; set; }
        
        // From robot to frontend
        public DbSet<MissionQueuesResponse> MissionQueuesResponse { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}