using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Model
{
    public class PropertyType
    {
        [Required,Key]
        public int PropertyTypeID { get; set; }
        [Required]
        public string PropertyTypeName { get; set; } = String.Empty;
    }
}
