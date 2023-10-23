using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data.Abstract;
using System.Linq.Expressions;

namespace RealEstateBE.Data.Concrete
{
    public class Repository<TEntity> :RepositoryReadWrite<TEntity>, IRepository<TEntity> where TEntity : class
    {
        public async Task<bool> DeleteByIdAsync(int id)
        {
                TEntity? t = await _entities.FindAsync(id);
                if (t != null)
                {
                   _entities.Remove(t);
                }
                return SaveChanges();
        }

        

        public bool Update(TEntity entity)
        {
            _entities.Update(entity);
            return SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        
    }
}
