using RealEstateEntities.Entities.DTOs.Property;
using RealEstateEntities.Entities;

namespace RealEstateService.Abstract
{
    public interface IPropertyService
    {
        public Task<IEnumerable<Property>> GetProperties();
        public Task<IEnumerable<Property>> FilterPropertiesAsync(PropertyFilterDTO propertyFilterDTO);
        public Task<Property?> GetProperty(int id);
        public Task<Property?> InsertProperty(PropertyDTO propertyDTO);
        public Task<Property?> UpdateProperty(PropertyDTO propertyDTO,int propertyId);
        public Task<bool> DeleteProperty(int id);
    }
}
