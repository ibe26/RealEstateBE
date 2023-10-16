using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs
{
    public class PropertyDTO
    {
        [Required]
        public string PropertyName { get; set; } = String.Empty;
        [Required]
        public int PropertyType { get; set; }
        [Required]
        public int PropertyListingType { get; set; }
        [Required]
        public int PropertyPrice { get; set; }
    }
}
