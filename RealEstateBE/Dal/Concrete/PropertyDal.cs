using Microsoft.EntityFrameworkCore;
using RealEstateBE.Dal.Abstract;
using RealEstateBE.Data.Concrete;
using RealEstateBE.Entities;

namespace RealEstateBE.Dal.Concrete
{
    public class PropertyDal:Repository<Property>,IPropertyDal
    {
        public override async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _entities.Include(p=>p.PropertyType).Include(p=>p.PropertyListingType).OrderByDescending(p=>p.DateListed).ToListAsync();
        }
        public override async Task<Property?> GetByIdAsync(int id)
        {
             if (id > 0)
            {
                return await _entities.Include(p => p.PropertyType).Include(p => p.PropertyListingType).SingleOrDefaultAsync(p=>p.PropertyID==id);
            }
            return null;
        }
    }
}
