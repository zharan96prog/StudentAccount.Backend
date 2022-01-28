using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using StudentAccount.DataAccess;
using StudentAccount.DataAccess.Entity;
using StudentAccount.Domain.ApiModel.RequestApiModels;
using StudentBook.Domain.ApiModel.RequestApiModels;
using StudentBook.Domain.ApiModel.ResponseApiModels;
using StudentBook.Domain.Errors;
using StudentBook.Domain.Interfaces;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StudentBook.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly EFContext _context;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService, IMapper mapper, EFContext context, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
        }

        public async Task<ResponseApiModel<HttpStatusCode>> RegisterUser(RegisterRequestApiModel userRequest)
        {
            var user = _mapper.Map<AppUser>(userRequest);
            user.EmailConfirmed = true;

            var resultUser = await _userManager.CreateAsync(user, userRequest.Password);

            if (resultUser.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(user, Roles.Roles.userRole);

                if (!resultRole.Succeeded) 
                {
                    return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.BadRequest, false, Resources.ResourceManager.GetString("registrationFailed"));
                }


                //var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                //var param = new Dictionary<string, string>
                //    {
                //        {"token",validEmailToken },
                //        {"email",user.Email }
                //    };

                //var callback = QueryHelpers.AddQueryString(userRequest.ClientURI, param);
                //var emailResult = await _emailService.SendEmailAsync(user.Email, "StudentAccount-Confirm Your Email", callback);

                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("registrationSuccessful"));
            }    
            
            return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.BadRequest, false, Resources.ResourceManager.GetString("registrationFailed"));
        }

        public async Task<AuthenticateResponseApiModel> LoginUser(LoginRequestApiModel userRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(userRequest.Email, userRequest.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userRequest.Email);

                var roles = await _userManager.GetRolesAsync(user);
                var token = await _jwtService.CreateJwtToken(user);
                var refreshtoken = _jwtService.CreateRefreshToken();

                user.RefreshTokens = refreshtoken;

                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, false);

                return new AuthenticateResponseApiModel(user.Email, token, refreshtoken.Token, roles.FirstOrDefault());
            }
            else throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("LoginWrongCredentials"));
        }

        public async Task<ResponseApiModel<HttpStatusCode>> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.Unauthorized, Resources.ResourceManager.GetString("UserNotFound"));
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("EmailConfirmedSuccessfully"));
            }
            else
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("LoginWrongCredentials"));
            }
        }

        public async Task<HttpStatusCode> UpdateUser(UpdateUserModel updateUserModel)
        {
            var user = await _userManager.FindByIdAsync(updateUserModel.Id);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "User not found");
            }

            user.FirstName = updateUserModel.FirstName;
            user.LastName = updateUserModel.LastName;
            user.Age = updateUserModel.Age;
            user.Email = updateUserModel.Email;
            user.UserName = updateUserModel.Email;

            var success = await _context.SaveChangesAsync() > 0;
            if (!success)
            {
                throw new RestException(HttpStatusCode.BadRequest, "User isn't updated" ); ;
            }
            return HttpStatusCode.OK;
        }
    }
}
