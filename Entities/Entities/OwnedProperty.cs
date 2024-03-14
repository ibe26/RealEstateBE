using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities
{
    public class OwnedProperty
    {

        [Required, Key]
        public int PropertyID { get; set; }

        [Required]
        public required string PropertyName { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public int PropertyPrice { get; set; }

        [Required]
        public int GrossArea { get; set; }

        [Required]
        public int NetArea { get; set; }

        [Required]
        public int GrossIncome { get; set; }
        public int NetIncome { get; set; }

        [Required]
        public int PropertyTypeID { get; set; }
        [Required]
        public virtual PropertyType PropertyType { get; set; }

        [Required]
        public int UserID { get; set; }
 
    }
}

