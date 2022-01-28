using StudentAccount.Domain.ApiModel.RequestApiModels;
using StudentBook.Domain.ApiModel.RequestApiModels;
using StudentBook.Domain.ApiModel.ResponseApiModels;
using System.Net;
using System.Threading.Tasks;

namespace StudentBook.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterRequestApiModel user);
        Task<AuthenticateResponseApiModel> LoginUser(LoginRequestApiModel userRequest);
        Task<ResponseApiModel<HttpStatusCode>> ConfirmEmailAsync(string userId, string token);
        Task<HttpStatusCode> UpdateUser(UpdateUserModel updateUserModel);
    }
}
