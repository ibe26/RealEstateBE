using RealEstateBE.Entities.DTOs;
using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Model
{
    public class Property
    {
        [Required, Key]
        public int PropertyID { get; set; }
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
