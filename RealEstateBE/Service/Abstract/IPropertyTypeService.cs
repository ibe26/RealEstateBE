using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data;
using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;

namespace RealEstateBE.Service.Abstract
{
    public interface IPropertyTypeService
    {

        public Task<IEnumerable<PropertyType>> GetPropertyTypes();
        public Task<PropertyType?> GetPropertyTypeById(int id);
        public Task<bool> InsertPropertyType(PropertyTypeDTO propertyTypeDTO);
        public Task<bool> DeletePropertyType(int id);

    }
}
