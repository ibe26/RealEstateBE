using RealEstateDataAccessLayer.Data.Abstract;
using RealEstateEntities.Entities;

namespace RealEstateDataAccessLayer.Abstract
{
    public interface IPropertyDal:IRepository<Property>
    {
        //Task<Property?> GetByIdAsync(Guid guid);
        //Task<bool> DeleteByIdAsync(Guid guid);
    }
}
