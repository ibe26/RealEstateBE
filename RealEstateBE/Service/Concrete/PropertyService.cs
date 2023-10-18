using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;
using RealEstateBE.Dal.Abstract;
using RealEstateBE.Service.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace RealEstateBE.Service.Concrete
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyDal _propertyDal;

        public PropertyService(IPropertyDal propertyDal)
        {
            _propertyDal = propertyDal;
        }
        public async Task<bool> DeleteProperty(int id)
        {
            Property? property = await _propertyDal.GetByIdAsync(id);
            return (property == null) ? false : (await _propertyDal.DeleteByIdAsync(id));
        }

        public async Task<IEnumerable<Property>> FilterPropertiesAsync(PropertyFilterDTO propertyFilterDTO)
        {
            if (propertyFilterDTO.MaxPrice == 0) propertyFilterDTO.MaxPrice = int.MaxValue;
            IEnumerable<Property> filteredProperties = await _propertyDal.WhereAsync(p =>
            (propertyFilterDTO.PropertyName.IsNullOrEmpty() ? true : p.PropertyName.ToLower().Contains(propertyFilterDTO.PropertyName.ToLower())) &&
            (propertyFilterDTO.PropertyType == 0 ? true : p.PropertyType == propertyFilterDTO.PropertyType) &&
            (propertyFilterDTO.PropertyListingType == 0 ? true : p.PropertyListingType == propertyFilterDTO.PropertyListingType) &&
            (p.PropertyPrice >= propertyFilterDTO.MinPrice) &&
            (p.PropertyPrice <= propertyFilterDTO.MaxPrice)

            );
            return filteredProperties;
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
            return await _propertyDal.GetAllAsync();
        }

        public async Task<Property?> GetProperty(int id)
        {
            return await _propertyDal.GetByIdAsync(id);
        }

        public async Task<bool> InsertProperty(PropertyDTO propertyDTO)
        {
            Property property = new()
            {
                PropertyName = propertyDTO.PropertyName,
                PropertyType = propertyDTO.PropertyType,
                PropertyPrice = propertyDTO.PropertyPrice,
                PropertyListingType = propertyDTO.PropertyListingType,
            };
            return await _propertyDal.AddAsync(property);
        }

        public async Task<bool> UpdateProperty(PropertyDTO propertyDTO, int propertyId)
        {
            Property? property = await _propertyDal.GetByIdAsync(propertyId);
            if (property != null)
            {
                property.PropertyName = propertyDTO.PropertyName;
                property.PropertyType = propertyDTO.PropertyType;
                property.PropertyPrice = propertyDTO.PropertyPrice;
                property.PropertyListingType = propertyDTO.PropertyListingType;
                return _propertyDal.Update(property);
            }
            else return false;
            
        }
    }
}
