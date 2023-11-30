using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs.Property
{
    public class PropertyTypeDTO
    {
        [Required]
        public string PropertyTypeName { get; set; } = string.Empty;
    }
}
