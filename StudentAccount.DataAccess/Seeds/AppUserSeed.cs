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
                        FirstName = "Alex",
                        LastName = "Zimin",
                        Email = "alexZ@gmail.com",
                        UserName = "alexZ@gmail.com",
                        Age = 27,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80956395845",
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
                    },
                    new AppUser
                    {
                        FirstName = "Olga",
                        LastName = "Minich",
                        Email = "olga@gmail.com",
                        UserName = "olga@gmail.com",
                        Age = 21,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80928523589",
                        RegisteredDate = DateTime.UtcNow
                    },
                    new AppUser
                    {
                        FirstName = "Lilia",
                        LastName = "Minchuk",
                        Email = "lilia@gmail.com",
                        UserName = "lilia@gmail.com",
                        Age = 20,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80925231589",
                        RegisteredDate = DateTime.UtcNow
                    },
                    new AppUser
                    {
                        FirstName = "Liza",
                        LastName = "Oleksiivec",
                        Email = "liza@gmail.com",
                        UserName = "liza@gmail.com",
                        Age = 25,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80925623589",
                        RegisteredDate = DateTime.UtcNow
                    },
                    new AppUser
                    {
                        FirstName = "Pavel",
                        LastName = "Dehtiar",
                        Email = "pavel@gmail.com",
                        UserName = "pavel@gmail.com",
                        Age = 31,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80920123589",
                        RegisteredDate = DateTime.UtcNow
                    },
                    new AppUser
                    {
                        FirstName = "Orest",
                        LastName = "Polykhovych",
                        Email = "ores@gmail.com",
                        UserName = "orest@gmail.com",
                        Age = 18,
                        RefreshTokens = null,
                        EmailConfirmed = true,
                        PhoneNumber = "80925258589",
                        RegisteredDate = DateTime.UtcNow
                    }
                };
                var userAdmin = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
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
