using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data;
using RealEstateBE.Model;

namespace RealEstateBE.Service.Abstract
{
    public interface IPropertyTypeService:ISaveContext
    {

        public Task<IEnumerable<PropertyType>> GetPropertyTypes();
        public Task<PropertyType> GetPropertyById(int id);
        public Task InsertPropertyType(PropertyType propertyType);
        public Task DeletePropertyType(int id);

    }
}
