using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamProject.Models;

namespace TeamProject.Data
{
    //part 11 identity framework
    //1. seeding roles and admin user
    public class IdentityDbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string adminUserPW)
        {
            //1. initialize custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //2. These will be the roles 
            string[] roleNames = { "Admin"};

            //3. Prepare result var
            IdentityResult roleResult;

            //4. Loop the roleName array - check if role already exists and create new role if neccessary
            foreach (var roleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    //role not exists
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            ////create admin user
            ////1. initialize custom users 
            //var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            ////2. check if the admin already exists
            //User adminUser = await userManager.FindByEmailAsync("admin@travel.com");

            //if (adminUser == null)
            //{
            //    //admin user does not exists create it
            //    adminUser = new User()
            //    {
            //        UserName = "admin@travel.com",
            //        Email = "admin@travel.com"
            //    };

            //    //actually create
            //    await userManager.CreateAsync(adminUser, adminUserPW);
            //    //the adminuserpw is for storing the password - it will be passed in via
            //    //start up (dependency Injection). the actual password will kept in a file called secrets.json

            //    //assign admin user to admin roles
            //    await userManager.AddToRoleAsync(adminUser, "Admin");

            //    //manually confirm user email
            //    var code = await userManager.GenerateEmailConfirmationTokenAsync(adminUser);
            //    var result = await userManager.ConfirmEmailAsync(adminUser, code);

            //    //no account for admin user
            //    await userManager.SetLockoutEnabledAsync(adminUser, false);
            //}

        }
    }
}
