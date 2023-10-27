using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;

namespace RealEstateBE.Service.Abstract
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
