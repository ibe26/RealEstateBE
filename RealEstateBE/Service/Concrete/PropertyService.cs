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
            if(propertyFilterDTO.MaxPrice==0) propertyFilterDTO.MaxPrice = int.MaxValue;
            IEnumerable<Property> filteredProperties = await _propertyDal.WhereAsync(p =>
            (propertyFilterDTO.PropertyName.IsNullOrEmpty() ? false : p.PropertyName.ToLower().Contains(propertyFilterDTO.PropertyName.ToLower())) &&
            (p.PropertyPrice >= propertyFilterDTO.MinPrice) &&
            (p.PropertyPrice <= propertyFilterDTO.MaxPrice) &&
            (propertyFilterDTO.PropertyType == 0 ? true : p.PropertyType == propertyFilterDTO.PropertyType) &&
            (propertyFilterDTO.PropertyListingType == 0 ? true : p.PropertyListingType == propertyFilterDTO.PropertyListingType)
            );
            return filteredProperties;
        }

        public Task<IEnumerable<Property>> GetProperties()
        {
            throw new NotImplementedException();
        }

        public Task<Property> GetProperty(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertProperty(PropertyDTO propertyDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProperty(PropertyDTO propertyDTO)
        {
            throw new NotImplementedException();
        }
    }
}
