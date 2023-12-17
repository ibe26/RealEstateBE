using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities
{
    public class PropertyType
    {
        [Required,Key]
        public int PropertyTypeID { get; set; }
        [Required]
        public string PropertyTypeName { get; set; } = String.Empty;
    }
}
