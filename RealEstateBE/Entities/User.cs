using RealEstateBE.Entities.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities
{
    public class User
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public byte[] PasswordKey { get; set; }
    }
}
