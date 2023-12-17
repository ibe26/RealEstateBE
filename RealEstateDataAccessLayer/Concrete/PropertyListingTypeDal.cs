using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Data.Concrete;
using RealEstateEntities.Entities;

namespace RealEstateDataAccessLayer.Concrete
{
    public class PropertyListingTypeDal:RepositoryReadWrite<PropertyListingType>,IPropertyListingTypeDal
    {
    }
}
