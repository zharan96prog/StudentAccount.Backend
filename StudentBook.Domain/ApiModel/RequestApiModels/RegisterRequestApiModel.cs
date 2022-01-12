﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBook.Domain.ApiModel.RequestApiModels
{
    public class RegisterRequestApiModel
    {
        /// <summary>
        /// User email address. Used for login.
        /// </summary>     
        /// <example>test@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// First name.
        /// </summary>     
        /// <example>TestName</example>
        public string FirstName { get; set; }
        /// <summary>
        /// First name.
        /// </summary>     
        /// <example>TestName</example>
        public string LastName { get; set; }
        /// <summary>
        /// User password. Must have at least one non alphanumeric character.
        /// </summary>     
        /// <example>QWerty-1</example>
        public string Password { get; set; }
        /// <summary>
        /// Repeat user password. Must match password.
        /// </summary>     
        /// <example>QWerty-1</example>
        public string ConfirmPassword { get; set; }
        public string ClientURI { get; set; }
    }
}
