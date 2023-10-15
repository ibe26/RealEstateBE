using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Entities.DTOs
{
    public class PropertyTypeDTO
    {
        [Required]
        public string PropertyTypeName { get; set; } = String.Empty;
    }
}
