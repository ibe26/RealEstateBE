
using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data.Abstract;
using System.Linq.Expressions;

namespace RealEstateBE.Data.Concrete
{
    public class RepositoryReadWrite<TEntity>:IRepositoryReadWrite<TEntity> where TEntity:class
    {
        private static readonly DataContext _context = new();
        protected readonly DbSet<TEntity> _entities = _context.Set<TEntity>();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }
        public async virtual Task<bool> AddAsync(TEntity entity)
        {
            if (entity is not null)
            {
                await _entities.AddAsync(entity);
            }
            return SaveChanges();
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
