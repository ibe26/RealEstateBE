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

        public async Task<IEnumerable<PropertyType>> GetPropertyTypes()
        {
            //Making such operations to make 'Other' option always to be on the bottom, to ease frontend.
            List<PropertyType> list= (await _propertyTypeDal.GetAllAsync()).ToList();
            ServiceHelper.MoveToBottom(list, p => p.PropertyTypeName.ToLower() == "other");
            return list;

        }

        public async Task<bool> InsertPropertyType(PropertyTypeDTO propertyTypeDTO)
        {
            PropertyType propertyType=new()
            { 
                PropertyTypeName=propertyTypeDTO.PropertyTypeName,
            };
            return await _propertyTypeDal.AddAsync(propertyType);
        }

    }
}
