using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAccount.Domain.ApiModel.RequestApiModels
{
    public class User
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegisterDate { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
    }
}
