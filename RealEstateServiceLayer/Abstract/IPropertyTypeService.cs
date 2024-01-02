using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Data;
using RealEstateEntities.Entities.DTOs.Property;
using RealEstateEntities.Entities;

namespace RealEstateService.Abstract
{
    public interface IPropertyTypeService
    {

        public Task<IEnumerable<PropertyType>> GetPropertyTypes();
        public Task<PropertyType?> InsertPropertyType(PropertyTypeDTO propertyTypeDTO);

    }
}
