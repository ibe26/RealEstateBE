
namespace RealEstateEntities.Entities.DTOs.Property
{
    public class PropertyFilterDTO
    {
        public string? PropertyName { get; set; } = string.Empty;
        public int? PropertyTypeID { get; set; } = 0;
        public int? PropertyListingTypeID { get; set; } = 0;
        public int? MinPrice { get; set; } = 0;
        public int? MaxPrice { get; set; } = int.MaxValue;
        public short? BedroomCount { get; set; }
        public short? BathroomCount { get; set; }
        public string? City { get; set; } = string.Empty;
        public string? District { get; set; } = string.Empty;
        public string? Quarter { get; set; } = string.Empty;
        public bool? Balcony { get; set; }
        public string? HeatSystem { get; set; }
    }
}
