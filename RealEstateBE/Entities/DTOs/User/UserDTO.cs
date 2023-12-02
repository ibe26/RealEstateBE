using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs.User
{
    public class UserDTO
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
        public string Token { get; set; }
    }
}
