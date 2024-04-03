using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateEntities.Entities
{
    public class User
    {
        public User()
        {
            UserID = Guid.NewGuid();
        }
        [Required]
        public virtual Guid UserID { get; set; }
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

        public virtual ICollection<Property> ListedProperties { get; set; }

        public virtual ICollection<OwnedProperty> OwnedProperties { get; set; }
    }
}
