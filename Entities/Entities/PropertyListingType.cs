using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities
{
    public class PropertyListingType
    {
        [Required,Key]
        public int PropertyListingTypeID { get; set; }
        [Required]
        public string PropertyListingTypeName { get; set; }=String.Empty;
    }
}
