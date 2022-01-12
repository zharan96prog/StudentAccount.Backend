using StudentAccount.DataAccess.Entity;
using StudentBook.Domain.ApiModel.ResponseApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBook.Domain.Interfaces
{
    public interface IJwtService
    {        
        Task<string> CreateJwtToken(AppUser user);
        Task<AuthenticateResponseApiModel> RefreshTokenAsync(string token);
        RefreshToken CreateRefreshToken();
    }
}
