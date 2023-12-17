using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities.DTOs.User
{
    public class LoginDTO
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
