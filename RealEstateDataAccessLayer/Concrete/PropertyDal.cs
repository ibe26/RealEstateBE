using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Data.Concrete;
using RealEstateEntities.Entities;

namespace RealEstateDataAccessLayer.Concrete
{
    public class PropertyDal:Repository<Property>,IPropertyDal
    {
        public override async Task<bool> DeleteByIdAsync(object guid)
        {
            Property? property = await GetByIdAsync(guid);
            if (property != null)
            {
                _entities.Remove(property);
            }
            return SaveChanges()>0;
        }

        public override async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _entities.Include(p => p.PropertyListingType).Include(p => p.PropertyListingType).OrderByDescending(p => p.DateListed).ToListAsync();
        }

        public override async Task<Property?> GetByIdAsync(object guid)
        {
            return await _entities.Include(p => p.PropertyListingType).Include(p => p.PropertyListingType).OrderByDescending(p => p.DateListed).SingleOrDefaultAsync(p => p.PropertyID == new Guid(guid.ToString()!));
        } 
    }
}
