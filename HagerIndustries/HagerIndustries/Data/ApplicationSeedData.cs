using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HagerIndustries.Data
{
    public static class ApplicationSeedData
    {
        // Current Roles (permissions desc):
        // Admin
        // Supervisor
        // Employee

        public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            //Create Roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Supervisor", "Employee"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Create Users
            //Makes admin account
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            if (userManager.FindByEmailAsync("admin1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin1@outlook.com",
                    Email = "admin1@outlook.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                //Adds this acount to a role
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("supervisor1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "supervisor1@outlook.com",
                    Email = "supervisor1@outlook.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                //Adds this acount to a role
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Supervisor").Wait();
                }
            }

            if (userManager.FindByEmailAsync("employee1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "employee1@outlook.com",
                    Email = "employee1@outlook.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "password").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Employee").Wait();
                }
            }

            if (userManager.FindByEmailAsync("user1@outlook.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "user1@outlook.com",
                    Email = "user1@outlook.com"
                };
                _ = userManager.CreateAsync(user, "password").Result;
                //Not in any role
            }
        }
    }
}
