using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs
{
    public class PropertyFilterDTO
    {
        public string? PropertyName { get; set; } = String.Empty;
        public int? PropertyType { get; set; } = 0;
        public int? PropertyListingType { get; set; } = 0;
        public int? MinPrice { get; set; } = 0;
        public int? MaxPrice { get; set; } = int.MaxValue;
        public Int16? BedroomCount { get; set; }
        public Int16? BathroomCount { get; set; }
        public int? Size { get; set; }
        public string? City { get; set; } = String.Empty;
        public string? District { get; set; } = String.Empty;
        public string Quarter { get; set; } = String.Empty;

    }
}
