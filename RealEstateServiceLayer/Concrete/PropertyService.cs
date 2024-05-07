using RealEstateEntities.Entities;
using RealEstateDataAccessLayer.Abstract;
using RealEstateService.Abstract;
using Microsoft.IdentityModel.Tokens;
using RealEstateEntities.Entities.DTOs.Property;

namespace RealEstateService.Concrete
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyDal _propertyDal;
        private readonly IPropertyTypeDal _propertyTypeDal;
        private readonly IPropertyListingTypeDal _propertyListingTypeDal;
        private readonly IUserDal _userDal;

        public PropertyService(IPropertyDal propertyDal,
                               IPropertyTypeDal propertyTypeDal,
                               IPropertyListingTypeDal propertyListingTypeDal,
                               IUserDal userDal)
        {
            _propertyDal = propertyDal;
            _propertyTypeDal = propertyTypeDal;
            _propertyListingTypeDal = propertyListingTypeDal;
            _userDal = userDal;
        }
        public async Task<bool> DeleteProperty(string _GUID)
        {
            Guid guid = new Guid(_GUID);
            Property? property = await _propertyDal.GetByIdAsync(guid);
            return (property == null) ? false : (await _propertyDal.DeleteByIdAsync(guid));
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
            (propertyFilterDTO.BedroomCount == 0 || p.BedroomCount == propertyFilterDTO.BedroomCount) &&
            (propertyFilterDTO.Balcony == null || p.Balcony == propertyFilterDTO.Balcony) &&
            (propertyFilterDTO.HeatSystem.IsNullOrEmpty() || p.HeatSystem.ToLower().Equals(propertyFilterDTO.HeatSystem!.ToLower())) &&
            (p.PropertyPrice >= propertyFilterDTO.MinPrice) &&
            (p.PropertyPrice <= propertyFilterDTO.MaxPrice) &&
            (p.OnListing == true)
            );
            return filteredProperties;
        }

        public async Task<IEnumerable<Property>> GetProperties()
        {
            return await _propertyDal.GetAllAsync();
        }

        public async Task<Property?> GetProperty(string _GUID)
        {
            Guid guid = new Guid(_GUID);
            return await _propertyDal.GetByIdAsync(guid);
        }

        public async Task<Property?> InsertProperty(PropertyDTO propertyDTO)
        {
            //PropertyTypeID and PropertyListingTypeID should be valid and exist in according to their database table.
            //So we must check whether these ID's exist in database or not. If not, return BadRequest.
            if ((await _propertyTypeDal.GetAllAsync()).Any(p => p.PropertyTypeID == propertyDTO.PropertyTypeID) &&
                (await _propertyListingTypeDal.GetAllAsync()).Any(p => p.PropertyListingTypeID == propertyDTO.PropertyListingTypeID) &&
                (await _userDal.GetByIdAsync(propertyDTO.UserID) != null))
            {
                Property property = new()
                {
                    PropertyName = propertyDTO.PropertyName,
                    UserID = propertyDTO.UserID,
                    PropertyTypeID = propertyDTO.PropertyTypeID,
                    PropertyListingTypeID = propertyDTO.PropertyListingTypeID,
                    PropertyPrice = propertyDTO.PropertyPrice,
                    City = propertyDTO.City,
                    District = propertyDTO.District,
                    Quarter = propertyDTO.Quarter,
                    GrossArea = propertyDTO.GrossArea,
                    NetArea = propertyDTO.NetArea,
                    BathroomCount = propertyDTO.BathroomCount,
                    BedroomCount = propertyDTO.BedroomCount,
                    DateListed = DateTime.Now,
                    Balcony = propertyDTO.Balcony,
                    Description = propertyDTO.Description,
                    Dues = propertyDTO.Dues,
                    HeatSystem = propertyDTO.HeatSystem,
                    BuiltYear = propertyDTO.BuiltYear,
                    TotalFloor = propertyDTO.TotalFloor,
                    Floor = propertyDTO.Floor,
                };
                return await _propertyDal.AddAsync(property);
            }
            return null;
        }

        public async Task<Property?> UpdateProperty(PropertyDTO propertyDTO, string propertyGUID)
        {
            Property? property = await _propertyDal.GetByIdAsync(new Guid(propertyGUID));
            if (property != null)
            {
                property.PropertyName = propertyDTO.PropertyName;
                property.UserID = propertyDTO.UserID;
                property.PropertyTypeID = propertyDTO.PropertyTypeID;
                property.PropertyListingTypeID = propertyDTO.PropertyListingTypeID;
                property.PropertyPrice = propertyDTO.PropertyPrice;
                property.City = propertyDTO.City;
                property.District = propertyDTO.District;
                property.Quarter = propertyDTO.Quarter;
                property.GrossArea = propertyDTO.GrossArea;
                property.NetArea = propertyDTO.NetArea;
                property.Balcony = propertyDTO.Balcony;
                property.Description = propertyDTO.Description;
                property.HeatSystem = propertyDTO.HeatSystem;
                property.Dues = propertyDTO.Dues;
                property.BuiltYear = propertyDTO.BuiltYear;
                property.Floor = propertyDTO.Floor;
                property.TotalFloor = propertyDTO.TotalFloor;
                property.OnListing = propertyDTO.OnListing;
                property.BathroomCount = propertyDTO.BathroomCount;
                property.BedroomCount = propertyDTO.BedroomCount;

                return _propertyDal.Update(property);
            }
            else return null;

        }
    }
}
