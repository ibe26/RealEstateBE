
namespace RealEstateEntities.Entities.DTOs.User
{
    public class UserDTO
    {
        public required int UserID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
        public required IEnumerable<Entities.Property> Properties { get; set; }

    }
}
