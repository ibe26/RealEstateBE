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
            return await base._entities.Include(op=>op.PropertyType).ToListAsync();
        }
    }
}
