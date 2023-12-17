using Microsoft.AspNetCore.Mvc;
using RealEstateDataAccessLayer;
using RealEstateDataAccessLayer.Abstract;
using RealEstateEntities.Entities.DTOs.Property;
using RealEstateEntities.Entities;
using RealEstateService.Abstract;
using RealEstateBE.Service;

namespace RealEstateService.Concrete
{
    public class PropertyListingTypeService : IPropertyListingTypeService
    {
        private readonly IPropertyListingTypeDal _propertyListingTypeDal;

        public PropertyListingTypeService(IPropertyListingTypeDal propertyListingTypeDal)
        {
            _propertyListingTypeDal = propertyListingTypeDal;
        }

        public Task<PropertyListingType?>? GetPropertyListingTypeByID(int id)
        {
            if (id > 0)
            {
                return _propertyListingTypeDal.GetByIdAsync(id);
            }
            return null;
        }

        public async Task<IEnumerable<PropertyListingType>> GetPropertyListingTypes()
        {
            List<PropertyListingType> list = (await _propertyListingTypeDal.GetAllAsync()).ToList();
            ServiceHelper.MoveToBottom(list, p => p.PropertyListingTypeName.ToLower() == "other");
            return list;
        }
        public async Task<PropertyListingType?>? InsertPropertyListingType(PropertyListingTypeDTO propertyListingTypeDTO)
        {
            PropertyListingType propertyListingType = new() { PropertyListingTypeName = propertyListingTypeDTO.PropertyListingTypeName };
            return await _propertyListingTypeDal.AddAsync(propertyListingType);
        }
    }
}
