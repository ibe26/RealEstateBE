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
            (propertyFilterDTO.PropertyName.IsNullOrEmpty() || p.PropertyName.ToLower().Contains(propertyFilterDTO.PropertyName!.ToLower())) &&
            (propertyFilterDTO.PropertyTypeID == 0 || p.PropertyTypeID == propertyFilterDTO.PropertyTypeID) &&
            (propertyFilterDTO.PropertyListingTypeID == 0 || p.PropertyListingTypeID == propertyFilterDTO.PropertyListingTypeID) &&
            (propertyFilterDTO.City.IsNullOrEmpty() || p.City.ToLower().Equals(propertyFilterDTO.City!.ToLower())) &&
            (propertyFilterDTO.District.IsNullOrEmpty() || p.District.ToLower().Equals(propertyFilterDTO.District!.ToLower())) &&
            (propertyFilterDTO.Quarter.IsNullOrEmpty() || p.Quarter.ToLower().Equals(propertyFilterDTO.Quarter!.ToLower())) &&
            (p.PropertyPrice >= propertyFilterDTO.MinPrice) 
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
                PropertyTypeID = propertyDTO.PropertyTypeID,
                PropertyListingTypeID = propertyDTO.PropertyTypeID,
                PropertyPrice = propertyDTO.PropertyPrice,
                City = propertyDTO.City,
                District = propertyDTO.District,
                Quarter = propertyDTO.Quarter,
                Size = propertyDTO.Size,
                BathroomCount = propertyDTO.BathroomCount,
                BedroomCount= propertyDTO.BedroomCount,
            };
            return await _propertyDal.AddAsync(property);
        }

        public async Task<bool> UpdateProperty(PropertyDTO propertyDTO, int propertyId)
        {
            Property? property = await _propertyDal.GetByIdAsync(propertyId);
            if (property != null)
            {
                property.PropertyName = propertyDTO.PropertyName;
                property.PropertyTypeID = propertyDTO.PropertyTypeID;
                property.PropertyListingTypeID = propertyDTO.PropertyTypeID;
                property.PropertyPrice = propertyDTO.PropertyPrice;
                property.City = propertyDTO.City;
                propertyDTO.District = propertyDTO.District;
                propertyDTO.Quarter = propertyDTO.Quarter;
                propertyDTO.Size = propertyDTO.Size;
                return _propertyDal.Update(property);
            }
            else return false;

        }
    }
}
