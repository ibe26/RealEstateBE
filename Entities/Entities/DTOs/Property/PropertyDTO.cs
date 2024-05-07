using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities.DTOs.Property
{
    public class PropertyDTO
    {
        
        public string PropertyName { get; set; } = string.Empty;
        
        public Guid UserID { get; set; }
        
        public int PropertyTypeID { get; set; }
        
        public int PropertyListingTypeID { get; set; }
        
        public int PropertyPrice { get; set; }
        
        public short BedroomCount { get; set; }
        
        public short BathroomCount { get; set; }
        
        public bool Balcony { get; set; }
        
        public int GrossArea { get; set; }
        
        public int NetArea { get; set; }
        
        public string City { get; set; } = string.Empty;
        
        public string District { get; set; } = string.Empty;
        
        public string Quarter { get; set; } = string.Empty;
        
        public int Dues { get; set; }
        
        public short BuiltYear { get; set; }
        public bool OnListing { get; set; }

        public string HeatSystem { get; set; } = string.Empty;
        public short? Floor { get; set; }
        public short? TotalFloor { get; set; }
        public string? Description { get; set; } = string.Empty;

    }
}
