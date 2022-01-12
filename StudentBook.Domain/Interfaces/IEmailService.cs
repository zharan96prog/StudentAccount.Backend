using StudentBook.Domain.ApiModel.ResponseApiModels;
using System.Net;
using System.Threading.Tasks;

namespace StudentBook.Domain.Interfaces
{
    public interface IEmailService
    {
        Task<ResponseApiModel<HttpStatusCode>> SendEmailAsync(string userEmail, string emailSubject, string message);
    }
}
