using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Data.Concrete;
using RealEstateEntities.Entities;

namespace RealEstateDataAccessLayer.Concrete
{
    public class PropertyDal:Repository<Property>,IPropertyDal
    {
        public async Task<bool> DeleteByIdAsync(Guid guid)
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
            return await _entities.Include(p=>p.PropertyListingType).Include(p=>p.PropertyListingType).OrderByDescending(p => p.DateListed).ToListAsync();
        }
        
        public async Task<Property?> GetByIdAsync(Guid guid)
        {
            return await _entities.FindAsync(guid);
        } 
    }
}
