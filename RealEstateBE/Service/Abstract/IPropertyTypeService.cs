using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data;
using RealEstateBE.Entities.DTOs.Property;
using RealEstateBE.Entities;

namespace RealEstateBE.Service.Abstract
{
    public interface IPropertyTypeService
    {

        public Task<IEnumerable<PropertyType>> GetPropertyTypes();
        public Task<PropertyType>? InsertPropertyType(PropertyTypeDTO propertyTypeDTO);

    }
}
