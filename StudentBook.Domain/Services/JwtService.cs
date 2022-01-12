using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using StudentAccount.DataAccess.Entity;
using StudentBook.Domain.ApiModel.ResponseApiModels;
using StudentBook.Domain.Errors;
using StudentBook.Domain.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using StudentAccount.DataAccess;

namespace StudentBook.Domain.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly EFContext _context;

        public JwtService(UserManager<AppUser> userManager, IConfiguration configuration, EFContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> CreateJwtToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Secret"));

            var userRoles = await _userManager.GetRolesAsync(user);
            //var role = userRoles.FirstOrDefault();
           // var role = _context.Users.FirstOrDefault(x => x.Email == user.Email).Role;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id",user.Id.ToString()),
                    //new Claim(ClaimTypes.Role,role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<double>("TokenExpires")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "http://localhost:44358",
                IssuedAt = DateTime.UtcNow
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthenticateResponseApiModel> RefreshTokenAsync(string token)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.RefreshTokens.Token == token);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("UserRefreshTokenNotFound"));
            }

            var refreshToken = user.RefreshTokens;

            if (!refreshToken.IsActive)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("UserRefreshTokenNotActive"));
            }

            var roles = await _userManager.GetRolesAsync(user);
            var newRefreshToken = CreateRefreshToken();
            refreshToken.Revoked = DateTime.UtcNow;
            user.RefreshTokens = newRefreshToken;

            await _userManager.UpdateAsync(user);

            var JWTToken = await CreateJwtToken(user);

            return new AuthenticateResponseApiModel(user.Email, JWTToken, newRefreshToken.Token, roles.FirstOrDefault());
        }

        public RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);

                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(_configuration.GetValue<double>("RefreshTokenExpires")),
                    Created = DateTime.UtcNow
                };
            }
        }
    }
}
