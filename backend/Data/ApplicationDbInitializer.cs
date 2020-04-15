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
        private static void CreateUsersAndRoles(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Create administrator roles
            var adminRole = new IdentityRole(Role.Admin);
            roleManager.CreateAsync(adminRole).Wait();

            // Create a user role
            var userRole = new IdentityRole(Role.User);
            roleManager.CreateAsync(userRole).Wait();

            // Create administrator users
            var admin = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Strator",
                UserName = "admin",
                Email = "admin@mail.com",
                Role = Role.Admin
            };
            userManager.CreateAsync(admin, "Password1.").Wait();
            userManager.AddToRoleAsync(admin, Role.Admin).Wait();

            // Create user
            var user = new ApplicationUser
            {
                FirstName = "User",
                LastName = "Nami",
                UserName = "user",
                Email = "user@mail.com",
                Role = Role.User
            };
            userManager.CreateAsync(user, "Password1.").Wait();
            userManager.AddToRoleAsync(user, Role.User).Wait();
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