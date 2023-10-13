using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data;
using RealEstateBE.Model;

namespace RealEstateBE.Service.Abstract
{
    public interface IPropertyTypeService:ISaveContext
    {

        public Task<IEnumerable<PropertyType>> GetPropertyTypes();
        public Task<PropertyType?> GetPropertyTypeById(int id);
        public Task<bool> InsertPropertyType(PropertyType propertyType);
        public Task<bool> DeletePropertyType(int id);

    }
}
