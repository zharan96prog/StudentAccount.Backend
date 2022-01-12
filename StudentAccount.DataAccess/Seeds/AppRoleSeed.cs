using Microsoft.AspNetCore.Identity;
using StudentAccount.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAccount.DataAccess.Seeds
{
    public class AppRoleSeed
    {
        public static async Task SeedRoleData(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<AppRole>
                {
                    new AppRole
                    {
                        Name = "admin"
                    },
                   new AppRole
                    {
                        Name = "user"
                    }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

        }
    }
}
