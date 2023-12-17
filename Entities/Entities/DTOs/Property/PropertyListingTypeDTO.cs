using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities.DTOs.Property
{
    public class PropertyListingTypeDTO
    {
        [Required]
        public string PropertyListingTypeName { get; set; } = string.Empty;
    }
}
