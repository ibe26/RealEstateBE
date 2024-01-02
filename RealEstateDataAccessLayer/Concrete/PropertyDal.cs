using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Data.Concrete;
using RealEstateEntities.Entities;

namespace RealEstateDataAccessLayer.Concrete
{
    public class PropertyDal:Repository<Property>,IPropertyDal
    {
        public override async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _entities.OrderByDescending(p => p.DateListed).ToListAsync();
        }
        
    }
}
