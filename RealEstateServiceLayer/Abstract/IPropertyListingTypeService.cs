using RealEstateEntities.Entities.DTOs.Property;
using RealEstateEntities.Entities;

namespace RealEstateService.Abstract
{
    public interface IPropertyListingTypeService
    {
        public Task<IEnumerable<PropertyListingType>> GetPropertyListingTypes();
        public Task<PropertyListingType?>? GetPropertyListingTypeByID(int id);
        public Task<PropertyListingType?>? InsertPropertyListingType(PropertyListingTypeDTO propertyListingTypeDTO);
    }
}
