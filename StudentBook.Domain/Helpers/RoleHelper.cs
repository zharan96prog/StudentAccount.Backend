using Microsoft.AspNetCore.Identity;
using StudentAccount.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAccount.Domain.Helpers
{
    public class RoleHelper
    {
        public async static Task<string> GetUserRoleAsync(UserManager<AppUser> userManager, AppUser user)
        {
            var role = await userManager.GetRolesAsync(user);
            var roleUser = role.FirstOrDefault();
            return roleUser;
        }
    }
}
