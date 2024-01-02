using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Data;
using RealEstateEntities.Entities.DTOs.Property;
using RealEstateEntities.Entities;
using RealEstateService.Abstract;
using RealEstateBE.Service;

namespace RealEstateService.Concrete
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

        public async Task<PropertyType?> InsertPropertyType(PropertyTypeDTO propertyTypeDTO)
        {
            PropertyType propertyType=new()
            { 
                PropertyTypeName=propertyTypeDTO.PropertyTypeName,
            };
            return await _propertyTypeDal.AddAsync(propertyType);
        }

    }
}
