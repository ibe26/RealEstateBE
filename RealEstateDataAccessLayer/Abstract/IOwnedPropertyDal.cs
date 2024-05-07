using RealEstateDataAccessLayer.Data.Abstract;
using RealEstateEntities.Entities;

namespace RealEstateDataAccessLayer.Abstract
{
    public interface IOwnedPropertyDal:IRepository<OwnedProperty>
    {
        public Task<IEnumerable<OwnedProperty>> GetAllAsync(string userGUID);
    }
}
