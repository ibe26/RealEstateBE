using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities.DTOs.Property
{
    public class OwnedPropertyDTO
    {
        
        public required string PropertyName { get; set; }
        public required int PropertyPrice { get; set; }
        
        public required int GrossArea { get; set; }

        public required int NetArea { get; set; }

        public required int GrossIncome { get; set; }
        public int NetIncome { get; set; }
        public required int PropertyTypeID { get; set; }
        public required int UserID { get; set; }
    }
}
