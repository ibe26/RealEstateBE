using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Model
{
    public class Property
    {
        [Required, Key]
        public int PropertyID { get; set; }
        [Required]
        public string PropertyName { get; set; } = String.Empty;
        [Required]
        public int PropertyType { get; set; }
        [Required]
        public int PropertyListingType { get; set; }
        [Required]
        public int PropertyPrice { get; set; }
    }
}
