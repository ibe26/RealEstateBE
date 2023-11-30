using RealEstateBE.Entities.DTOs.Property;
using RealEstateBE.Entities;

namespace RealEstateBE.Service.Abstract
{
    public interface IPropertyListingTypeService
    {
        public Task<IEnumerable<PropertyListingType>> GetPropertyListingTypes();
        public Task<PropertyListingType?>? GetPropertyListingTypeByID(int id);
        public Task<PropertyListingType?>? InsertPropertyListingType(PropertyListingTypeDTO propertyListingTypeDTO);
    }
}
