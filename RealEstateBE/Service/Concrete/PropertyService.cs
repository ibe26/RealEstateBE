﻿using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;
using RealEstateBE.Dal.Abstract;
using RealEstateBE.Service.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace RealEstateBE.Service.Concrete
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyDal _propertyDal;
        private readonly IPropertyTypeDal _propertyTypeDal;
        private readonly IPropertyListingTypeDal _propertyListingTypeDal;

        public PropertyService(IPropertyDal propertyDal,
                               IPropertyTypeDal propertyTypeDal,
                               IPropertyListingTypeDal propertyListingTypeDal)
        {
            _propertyDal = propertyDal;
            _propertyTypeDal = propertyTypeDal;
            _propertyListingTypeDal = propertyListingTypeDal;
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
            (propertyFilterDTO.TimeFilter==0 || (DateTime.Now.Day-propertyFilterDTO.TimeFilter)<=p.DateListed.Day) &&
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
            if (id > 0)
            {
                return await _propertyDal.GetByIdAsync(id);
            }
            return null;
        }

        public async Task<Property?> InsertProperty(PropertyDTO propertyDTO)
        {
            //PropertyTypeID and PropertyListingTypeID should be valid and exist in according to their database table.
            //So we must check whether these ID's exist in database or not. If not, return BadRequest.
            if ((await _propertyTypeDal.GetAllAsync()).Any(p => p.PropertyTypeID == propertyDTO.PropertyTypeID) &&
                (await _propertyListingTypeDal.GetAllAsync()).Any(p => p.PropertyListingTypeID == propertyDTO.PropertyListingTypeID))
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
                    GrossArea = propertyDTO.GrossArea,
                    NetArea = propertyDTO.NetArea,
                    BathroomCount = propertyDTO.BathroomCount,
                    BedroomCount = propertyDTO.BedroomCount,
                };
                var x = await _propertyDal.AddAsync(property);
                return x;
            }
            return null;
        }

        public async Task<Property?> UpdateProperty(PropertyDTO propertyDTO, int propertyId)
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
                propertyDTO.GrossArea = propertyDTO.GrossArea;
                propertyDTO.NetArea = propertyDTO.NetArea;
                return _propertyDal.Update(property);
            }
            else return null;

        }
    }
}
