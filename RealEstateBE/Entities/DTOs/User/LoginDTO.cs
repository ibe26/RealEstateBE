using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs.User
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
    }
}
