using System;
using backend.Models;
using System.Linq;
using backend.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public static class ApplicationDbInitializer
    {
        private static void CreateUsersAndRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create roles
            var adminRole = new IdentityRole(Role.Admin);
            roleManager.CreateAsync(adminRole).Wait();

            // Create users
            var admin = new ApplicationUser { UserName = "Admin", Email = "admin@mail.no", Role = Role.Admin};
            userManager.CreateAsync(admin, "Password1.").Wait();
            userManager.AddToRoleAsync(admin, Role.Admin).Wait();
        }
        
        public static void Init(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, bool development)
        {
            // Run migrations and add users if we're not in development mode
            if (!development)
            {
                context.Database.Migrate();

                // Only create users if no users exist
                if (!context.ApplicationUsers.Any())
                    CreateUsersAndRoles(userManager, roleManager);
                
                return;
            }

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            CreateUsersAndRoles(userManager, roleManager);
            context.SaveChangesAsync();
        }
    }
}