using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs.Property
{
    public class PropertyListingTypeDTO
    {
        [Required]
        public string PropertyListingTypeName { get; set; } = string.Empty;
    }
}
