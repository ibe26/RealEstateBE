using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs.User
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }
}
