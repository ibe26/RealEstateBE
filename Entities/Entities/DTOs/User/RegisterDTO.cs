using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities.DTOs.User
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }
}
