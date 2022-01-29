using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentAccount.DataAccess;
using StudentAccount.DataAccess.Entity;
using StudentAccount.Domain.ApiModel.RequestApiModels;
using StudentAccount.Domain.Helpers;
using StudentAccount.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAccount.Domain.Queries
{
    public class UserQueriesService : IUserQueriesService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly EFContext context;
        public UserQueriesService(UserManager<AppUser> userManager, EFContext context)
        {
            this.userManager = userManager;
            this.context = context;

        }

        public async Task<PaginationModel> GetAllUsers(MoreParametersModel model)
        {
            IQueryable<AppUser> users;
            users = SearchUsers(model.SearchParameter);
            users = users.AsQueryable().AsNoTracking();

            var allUsers = await users
                .Skip((model.numberPage - 1) * PaginationModel.DefaultPageSize)
                .Take(PaginationModel.DefaultPageSize).ToListAsync();
            var count = context.Users.Count();

            return new PaginationModel
            {
                Users = allUsers,
                PageNumber = count,
            };
        }

        public async Task<User> GetUserById(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            var role = await RoleHelper.GetUserRoleAsync(userManager, user);

            return new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,
                RegisterDate = user.RegisteredDate.ToString(),
                Role = role,
            };
        }
        public IQueryable<AppUser> SearchUsers(string searchParameter)
        {
            IQueryable<AppUser> users = null;
            if (!string.IsNullOrWhiteSpace(searchParameter))
            {
                users = context.AppUsers.Where(x => x.FirstName.ToLower().Contains(searchParameter.ToLower())
                    || x.LastName.ToLower().Contains(searchParameter.ToLower())
                    || (x.FirstName + ' ' + x.LastName).ToLower().Contains(searchParameter.ToLower())
                    || (x.LastName + ' ' + x.FirstName).ToLower().Contains(searchParameter.ToLower())
                    || (x.Age).ToString().Contains(searchParameter.ToLower())
                    || (x.Email).ToLower().Contains(searchParameter.ToLower()));
            }
            return users;
        }
    }
}
