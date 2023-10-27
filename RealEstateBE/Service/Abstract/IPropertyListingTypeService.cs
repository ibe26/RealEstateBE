using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;

namespace RealEstateBE.Service.Abstract
{
    public interface IPropertyListingTypeService
    {
        public Task<IEnumerable<PropertyListingType>> GetPropertyListingTypes();
        public Task<PropertyListingType?>? GetPropertyListingTypeByID(int id);
        public Task<PropertyListingType?>? InsertPropertyListingType(PropertyListingTypeDTO propertyListingTypeDTO);
    }
}
