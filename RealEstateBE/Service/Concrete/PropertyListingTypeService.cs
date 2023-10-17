using Microsoft.AspNetCore.Mvc;
using RealEstateBE.Controllers.Helper;
using RealEstateBE.Dal.Abstract;
using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;
using RealEstateBE.Service.Abstract;

namespace RealEstateBE.Service.Concrete
{
    public class PropertyListingTypeService : IPropertyListingTypeService
    {
        private readonly IPropertyListingTypeDal _propertyListingTypeDal;

        public PropertyListingTypeService(IPropertyListingTypeDal propertyListingTypeDal)
        {
            _propertyListingTypeDal = propertyListingTypeDal;
        }
        public async Task<IEnumerable<PropertyListingType>> GetPropertyListingTypes()
        {
            List<PropertyListingType> list= (await _propertyListingTypeDal.GetAllAsync()).ToList();
            ServiceHelper.MoveToBottom(list,p=>p.PropertyListingTypeName.ToLower()=="other");
            return list;
        }
        public async Task<bool> InsertPropertyListingType(PropertyListingTypeDTO propertyListingTypeDTO)
        {
            PropertyListingType propertyListingType= new() { PropertyListingTypeName=propertyListingTypeDTO.PropertyListingTypeName};
            return await _propertyListingTypeDal.AddAsync(propertyListingType);
        }
    }
}
