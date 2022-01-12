using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBook.Domain.ApiModel.RequestApiModels;
using StudentBook.Domain.Interfaces;
using System.Threading.Tasks;

namespace StudentAccount.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _serviceAccount;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthService serviceAccount, IJwtService jwtService)
        {
            _serviceAccount = serviceAccount;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestApiModel model)
        {
            var response = await _serviceAccount.LoginUser(model);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestApiModel userRegisterRequest)
        {
            var response = await _serviceAccount.RegisterUser(userRegisterRequest);
            return Ok(response);
        }
    }
}
