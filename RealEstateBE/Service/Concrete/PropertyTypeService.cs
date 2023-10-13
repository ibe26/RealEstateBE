using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data;
using RealEstateBE.Model;
using RealEstateBE.Service.Abstract;

namespace RealEstateBE.Service.Concrete
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly DataContext _dbContext;
        private DbSet<PropertyType> _propertyType;

        public PropertyTypeService(DataContext dbContext)
        {
            _dbContext = dbContext;
            _propertyType = dbContext.PropertyTypes;
        }
        public async Task<bool> DeletePropertyType(int id)
        {
            var entity =await _propertyType.SingleOrDefaultAsync(p => p.PropertyTypeID == id);
            if (entity != null)
            {
               _propertyType.Remove(entity);
            }
            return SaveChanges();

        }

        public async Task<PropertyType?> GetPropertyTypeById(int id)
        {
            if(id>0)
            {
                return await _propertyType.FindAsync(id);
            }
            return null;
        }

        public async Task<IEnumerable<PropertyType>> GetPropertyTypes()
        {
            return await _propertyType.ToListAsync();
        }

        public async Task<bool> InsertPropertyType(PropertyType propertyType)
        {
             await _propertyType.AddAsync(propertyType);
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges()>0;
        }
    }
}
