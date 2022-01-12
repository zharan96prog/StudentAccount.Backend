using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBook.Domain.ApiModel.RequestApiModels
{
    public class LoginRequestApiModel
    {
        /// <summary>
        /// User email
        /// </summary>     
        /// <example>test@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        /// <example>QWerty-1</example>
        public string Password { get; set; }
    }
}
