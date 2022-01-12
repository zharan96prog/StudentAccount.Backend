using Microsoft.AspNetCore.Identity;
using Roles;
using StudentAccount.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAccount.DataAccess.Seeds
{
    public static class AppUserSeed
    {
        public async static Task SeedUserData(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser> {
                    new AppUser
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com",
                        Age = 30,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80968695845",
                        RegisteredDate = DateTime.UtcNow
                    },
                    new AppUser
                    {
                        FirstName = "Alex",
                        LastName = "Zharan",
                        Email = "alex@gmail.com",
                        UserName = "alex@gmail.com",
                        Age = 25,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80985623589",
                        RegisteredDate = DateTime.UtcNow
                    },
                    new AppUser
                    {
                        FirstName = "Dima",
                        LastName = "Pinkevych",
                        Email = "dima@gmail.com",
                        UserName = "dima@gmail.com",
                        Age = 20,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80925623589",
                        RegisteredDate = DateTime.UtcNow
                    }
                };
                var userAdmin = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@studacc.com",
                    UserName = "admin@studacc.com",
                    Age = 20,
                    RegisteredDate = DateTime.UtcNow,
                    EmailConfirmed = true
                };

                var adminSuccessCreate = await userManager.CreateAsync(userAdmin, "Qwerty123");
                if (adminSuccessCreate.Succeeded)
                {
                    await userManager.AddToRoleAsync(userAdmin, Roles.Roles.adminRole);
                }
                foreach (var user in users)
                {
                    var successCreate = await userManager.CreateAsync(user, "Qwerty123");
                    if (successCreate.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, Roles.Roles.userRole);
                    }
                }
            }
        }
    }
}
