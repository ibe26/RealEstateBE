using RealEstateEntities.Entities;
using RealEstateEntities.Entities.DTOs.Property;

namespace RealEstateServiceLayer.Abstract
{
    public interface IOwnedPropertyService
    {
        public Task<IEnumerable<OwnedProperty>> GetOwnedProperties();
        public Task<OwnedProperty?> GetOwnedProperty(int id);
        public Task<OwnedProperty?> InsertOwnedProperty(OwnedPropertyDTO OwnedPropertyDTO);
        public Task<OwnedProperty?> UpdateOwnedProperty(OwnedPropertyDTO OwnedPropertyDTO, int OwnedPropertyId);
        public Task<bool> DeleteOwnedProperty(int id);
    }
}
