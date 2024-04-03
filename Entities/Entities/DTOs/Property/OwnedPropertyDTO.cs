using System.ComponentModel.DataAnnotations;

namespace RealEstateEntities.Entities.DTOs.Property
{
    public class OwnedPropertyDTO
    {
        
        public required string PropertyName { get; set; }
        public required int PropertyPrice { get; set; }
        public required int GrossArea { get; set; }
        public required int NetArea { get; set; }
        public required int PropertyTypeID { get; set; }
        public required string UserID { get; set; }
        public required int Yield { get; set;}
    }
}
