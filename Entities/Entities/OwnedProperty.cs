using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int Yield { get; set; }

        [Required]
        public int PropertyTypeID { get; set; }
        [Required]
        public virtual PropertyType PropertyType { get; set; }

        [Required]
        public Guid UserID { get; set; }
        [Required]


        [NotMapped]
        public double PriceYieldRatio
        {
            get
            {
                return Yield != 0 ? (double)Math.Truncate((decimal)PropertyPrice/Yield * 100) / 100 : 0;
            }
        }
    }
}

