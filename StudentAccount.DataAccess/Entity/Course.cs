using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentAccount.DataAccess.Entity
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public DateTime StartCourse { get; set; }
        [Required]
        public DateTime EndCourse { get; set; }

        public virtual Picture Pictures { get; set; }
        public virtual AppUser Users { get; set; }
    }
}
