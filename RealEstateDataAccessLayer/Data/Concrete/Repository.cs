using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Data.Abstract;
using System.Linq.Expressions;

namespace RealEstateDataAccessLayer.Data.Concrete
{
    public class Repository<TEntity> :RepositoryReadWrite<TEntity>, IRepositoryReadWrite<TEntity> where TEntity : class
    {
        public async Task<bool> DeleteByIdAsync(int id)
        {
                TEntity? t = await _entities.FindAsync(id);
                if (t != null)
                {
                   _entities.Remove(t);
                }
                return SaveChanges()>0;
        }

        

        public TEntity Update(TEntity entity)
        {
            _entities.Update(entity);
             SaveChanges();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        
    }
}
