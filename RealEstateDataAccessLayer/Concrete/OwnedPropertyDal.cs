using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Data.Concrete;
using RealEstateEntities.Entities;

namespace RealEstateDataAccessLayer.Concrete
{
    public class OwnedPropertyDal:Repository<OwnedProperty>,IOwnedPropertyDal
    {
        public override async Task<IEnumerable<OwnedProperty>> GetAllAsync()
        {
            return await _entities.Include(p => p.PropertyType).ToListAsync();
        }
        public async Task<IEnumerable<OwnedProperty>> GetAllAsync(string userGUID)
        {
            var _userGuid = new Guid(userGUID);
            return await _entities.Include(p => p.PropertyType).Where(p=>p.UserID==_userGuid).ToListAsync();
        }

        public override async Task<OwnedProperty?> GetByIdAsync(object guid)
        {
            return await _entities.Include(p => p.PropertyType).SingleOrDefaultAsync(p => p.PropertyID == (int)guid);
        }
    }
}
