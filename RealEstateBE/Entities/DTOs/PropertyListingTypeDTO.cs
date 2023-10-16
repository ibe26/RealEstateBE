using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs
{
    public class PropertyListingTypeDTO
    {
        [Required]
        public string PropertyListingTypeName { get; set; }=String.Empty;
    }
}
