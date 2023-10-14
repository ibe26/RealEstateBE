using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateBE.Dal.Abstract;
using RealEstateBE.Data;
using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;
using RealEstateBE.Service.Abstract;

namespace RealEstateBE.Service.Concrete
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IPropertyTypeDal _propertyTypeDal;

        public PropertyTypeService(IPropertyTypeDal propertyTypeDal)
        {
            _propertyTypeDal = propertyTypeDal;
        }


        public async Task<bool> DeletePropertyType(int id)
        {
            var entity =await _propertyTypeDal.SingleOrDefaultAsync(p => p.PropertyTypeID == id);
            if (entity == null)
            {
                return false;
            }
            return await _propertyTypeDal.DeleteByIdAsync(entity.PropertyTypeID);
        }

        public async Task<PropertyType?> GetPropertyTypeById(int id)
        {
            if(id>0)
            {
                return await _propertyTypeDal.GetByIdAsync(id);
            }
            return null;
        }

        public async Task<IEnumerable<PropertyType>> GetPropertyTypes()
        {
            return await _propertyTypeDal.GetAllAsync();
        }

        public async Task<bool> InsertPropertyType(PropertyTypeDTO propertyTypeDTO)
        {
            PropertyType propertyType=new PropertyType()
            { 
                PropertyTypeName=propertyTypeDTO.PropertyTypeName,
            };
            return await _propertyTypeDal.AddAsync(propertyType);
        }

    }
}
