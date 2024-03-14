using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Concrete;
using RealEstateEntities.Entities;
using RealEstateEntities.Entities.DTOs.Property;
using RealEstateServiceLayer.Abstract;

namespace RealEstateServiceLayer.Concrete
{
    public class OwnedPropertyService : IOwnedPropertyService
    {
        private readonly IOwnedPropertyDal _ownedPropertyDal;
        public OwnedPropertyService(IOwnedPropertyDal ownedPropertyDal)
        {
            _ownedPropertyDal = ownedPropertyDal;
        }

        public async Task<bool> DeleteOwnedProperty(int id)
        {
            return await _ownedPropertyDal.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<OwnedProperty>> GetOwnedProperties()
        {
            return await _ownedPropertyDal.GetAllAsync();
        }

        public async Task<OwnedProperty?> GetOwnedProperty(int id)
        {
            OwnedProperty? ownedProperty = await _ownedPropertyDal.GetByIdAsync(id);
            if (ownedProperty != null)
            {
                return ownedProperty;
            }
            throw new ArgumentException("No Property owned with corresponding id.");
        }

        public async Task<OwnedProperty?> InsertOwnedProperty(OwnedPropertyDTO? OwnedPropertyDTO)
        {
            if (OwnedPropertyDTO == null) throw new ArgumentNullException(nameof(OwnedPropertyDTO));
            OwnedProperty property = new OwnedProperty
            {
                DateAdded = DateTime.Now,
                PropertyName = OwnedPropertyDTO.PropertyName,
                PropertyTypeID = OwnedPropertyDTO.PropertyTypeID,
                GrossArea = OwnedPropertyDTO.GrossArea,
                GrossIncome = OwnedPropertyDTO.GrossIncome,
                NetArea = OwnedPropertyDTO.NetArea,
                NetIncome = OwnedPropertyDTO.NetIncome,
                UserID = OwnedPropertyDTO.UserID
            };
           
            return await _ownedPropertyDal.AddAsync(property);
        }

        public async Task<OwnedProperty?> UpdateOwnedProperty(OwnedPropertyDTO OwnedPropertyDTO, int OwnedPropertyId)
        {
            OwnedProperty property = await _ownedPropertyDal.GetByIdAsync(OwnedPropertyId) ?? throw new ArgumentNullException(nameof(OwnedPropertyDTO));
            property.PropertyName = OwnedPropertyDTO.PropertyName;
                property.UserID = OwnedPropertyDTO.UserID;
                property.PropertyTypeID = OwnedPropertyDTO.PropertyTypeID;
                property.GrossArea = OwnedPropertyDTO.GrossArea;
                property.NetArea = OwnedPropertyDTO.NetArea;
                property.PropertyTypeID= OwnedPropertyDTO.PropertyTypeID;
                property.PropertyPrice = OwnedPropertyDTO.PropertyPrice;
                return _ownedPropertyDal.Update(property);
        }
    }
}
