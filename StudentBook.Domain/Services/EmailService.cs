using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using StudentBook.Domain.ApiModel.ResponseApiModels;
using StudentBook.Domain.Errors;
using StudentBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StudentBook.Domain.Services
{
    public class EmailService : IEmailService
    {
        public EmailService() { }

        public async Task<ResponseApiModel<HttpStatusCode>> SendEmailAsync(string userEmail, string emailSubject, string message)
        {
            using SmtpClient smtp = new();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("studentAccount.mailservice@gmail.com", "oz02021996");
            smtp.EnableSsl = true;
            smtp.Timeout = 3000;

            var from = new MailAddress("smtp.alexzharan@gmail.com");
            var to = new MailAddress(userEmail);

            MailMessage mailMessage = new(from, to);
            mailMessage.Subject = emailSubject;
            mailMessage.Body = message;

            try
            {
                await smtp.SendMailAsync(mailMessage);
                return new ResponseApiModel<HttpStatusCode>(HttpStatusCode.OK, true, Resources.ResourceManager.GetString("EmailSend"));
            }
            catch (Exception)
            {
                throw new RestException(HttpStatusCode.BadRequest, Resources.ResourceManager.GetString("EmailNotSend"));
            }
        }
    }
}
