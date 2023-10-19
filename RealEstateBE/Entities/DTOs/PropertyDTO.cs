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
        [Required]
        public Int16 BedroomCount { get; set; }
        [Required]
        public Int16 BathroomCount { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public string City { get; set; } = String.Empty;
        [Required]
        public string District { get; set; } = String.Empty;
        [Required]
        public string Quarter { get; set; } = String.Empty;
 
    }
}
