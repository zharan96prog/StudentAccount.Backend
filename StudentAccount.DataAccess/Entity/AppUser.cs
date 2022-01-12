using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentAccount.DataAccess.Entity
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public DateTime RegisteredDate { get; set; }

        public virtual List<Course> Courses { get; set; }
        [JsonIgnore]
        public RefreshToken RefreshTokens { get; set; }
    }
}
