using backend.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public static class ApplicationDbInitializer
    {
        private static void CreateUsersAndRoles(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            // Create roles
            var adminRole = new IdentityRole("Admin");
            rm.CreateAsync(adminRole).Wait();

            // Create users
            var admin = new ApplicationUser { UserName = "admin@mail.com", Email = "admin@mail.no" };
            um.CreateAsync(admin, "Password1.").Wait();
            um.AddToRoleAsync(admin, "Admin").Wait();
        }
        
        public static void Init(ApplicationDbContext context, UserManager<ApplicationUser> um,
            RoleManager<IdentityRole> rm, bool development)
        {
            // Run migrations and add users if we're not in development mode
            if (!development)
            {
                context.Database.Migrate();

                // Only create users if no users exist
                if (!context.ApplicationUsers.Any())
                    CreateUsersAndRoles(um, rm);
                
                return;
            }

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            CreateUsersAndRoles(um, rm);
        }
    }
}