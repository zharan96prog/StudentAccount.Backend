using Microsoft.AspNetCore.Http;
using StudentAccount.DataAccess.Entity;
using StudentAccount.Domain.ApiModel.RequestApiModels;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAccount.Domain.Interfaces
{
    public interface IUserQueriesService
    {
        Task<User> EmailConfirmation(HttpContext httpContext);
        Task<PaginationModel> GetAllUsers(MoreParametersModel model);
        Task<User> GetUserById(string Id);
        IQueryable<AppUser> SearchUsers(string searchName);
    }
}
