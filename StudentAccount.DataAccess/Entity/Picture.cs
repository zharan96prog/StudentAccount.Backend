using System.ComponentModel.DataAnnotations;

namespace StudentAccount.DataAccess.Entity
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public byte[] Image { get; set; }
    }
}
