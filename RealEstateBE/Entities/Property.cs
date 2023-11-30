using RealEstateBE.Entities.DTOs.Property;
using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities
{
    public class Property : PropertyDTO
    {
        [Required, Key]
        public int PropertyID { get; set; }
        [Required]
        public DateTime DateListed { get; set; }
        [Required]
        public PropertyType PropertyType { get; set; }
        [Required]
        public PropertyListingType PropertyListingType { get; set; }

        //[Required]
        //public string PropertyName { get; set; } = String.Empty;
        //[Required]
        //public int PropertyTypeID { get; set; }
        //[Required]
        //public int PropertyListingTypeID { get; set; }
        //[Required]
        //public int PropertyPrice { get; set; }
        //[Required]
        //public Int16 BedroomCount { get; set; }
        //[Required]
        //public Int16 BathroomCount { get; set; }
        //[Required]
        //public int Size { get; set; }
        //[Required]
        //public string City { get; set; } = String.Empty;
        //[Required]
        //public string District { get; set; } = String.Empty;
        //[Required]
        //public string Quarter { get; set; } = String.Empty;

    }
}
