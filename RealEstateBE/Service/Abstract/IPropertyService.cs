using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;

namespace RealEstateBE.Service.Abstract
{
    public interface IPropertyService
    {
        public Task<IEnumerable<Property>> GetProperties();
        public Task<IEnumerable<Property>> FilterPropertiesAsync(PropertyFilterDTO propertyFilterDTO);
        public Task<Property> GetProperty(int id);
        public Task<bool> InsertProperty(PropertyDTO propertyDTO);
        public Task<bool> UpdateProperty(PropertyDTO propertyDTO);
        public Task<bool> DeleteProperty(int id);
    }
}
