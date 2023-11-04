using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs
{
    public class PropertyDTO
    {
        [Required]
        public string PropertyName { get; set; } = String.Empty;
        [Required]
        public int PropertyTypeID { get; set; }
        [Required]
        public int PropertyListingTypeID { get; set; }
        [Required]
        public int PropertyPrice { get; set; }
        [Required]
        public Int16 BedroomCount { get; set; }
        [Required]
        public Int16 BathroomCount { get; set; }
        [Required]
        public bool Balcony { get; set; }
        [Required]
        public int GrossArea { get; set; }
        [Required]
        public int NetArea { get; set; }
        [Required]
        public string City { get; set; } = String.Empty;
        [Required]
        public string District { get; set; } = String.Empty;
        [Required]
        public string Quarter { get; set; } = String.Empty;
        public string Description { get; set; }=String.Empty;

        [Required]
        public int Dues { get; set; }
        [Required]
        public string HeatSystem { get; set; } = String.Empty;
    }
}
