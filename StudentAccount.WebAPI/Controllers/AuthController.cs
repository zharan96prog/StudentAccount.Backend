using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentAccount.Domain.ApiModel.RequestApiModels;
using StudentAccount.Domain.Interfaces;
using StudentBook.Domain.ApiModel.RequestApiModels;
using StudentBook.Domain.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace StudentAccount.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _serviceAccount;
        private readonly IJwtService _jwtService;
        private readonly IUserQueriesService _queryUserService;

        public AuthController(IAuthService serviceAccount, IJwtService jwtService, IUserQueriesService queryUserService)
        {
            _serviceAccount = serviceAccount;
            _jwtService = jwtService;
            _queryUserService = queryUserService;
        }

        [AllowAnonymous]
        [HttpPost("login/")]
        public async Task<IActionResult> Login(LoginRequestApiModel model)
        {
            var response = await _serviceAccount.LoginUser(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register/")]
        public async Task<IActionResult> Register(RegisterRequestApiModel userRegisterRequest)
        {
            var response = await _serviceAccount.RegisterUser(userRegisterRequest);
            return Ok(response);
        }

        [Authorize(Roles = Roles.Roles.adminRole)]
        [HttpGet("adminList/")]
        public async Task<PaginationModel> GetAllUsers(MoreParametersModel model)
        {
            return await _queryUserService.GetAllUsers(model);
        }

        [Authorize(Roles = Roles.Roles.adminRole)]
        [HttpPut]
        public async Task<HttpStatusCode> UpdateUser(UpdateUserModel updateUserModel)
        {
            return await _serviceAccount.UpdateUser(updateUserModel);
        }
    }
}
