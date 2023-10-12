using System.ComponentModel.DataAnnotations;

namespace RealEstateBE.Model
{
    public class PropertyListingType
    {
        [Required,Key]
        public int PropertyListingTypeID { get; set; }
        [Required]
        public string PropertyListingTypeName { get; set; }=String.Empty;
    }
}
