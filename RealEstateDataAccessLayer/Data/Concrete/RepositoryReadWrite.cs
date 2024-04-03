
using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Data.Abstract;
using System.Linq.Expressions;

namespace RealEstateDataAccessLayer.Data.Concrete
{
    public class RepositoryReadWrite<TEntity>:IRepositoryReadWrite<TEntity> where TEntity:class
    {
        private static readonly DataContext _context = new();
        protected readonly DbSet<TEntity> _entities = _context.Set<TEntity>();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }
        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
                return await _entities.FindAsync(id);
        }
        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }
        public async virtual Task<TEntity?> AddAsync(TEntity entity)
        {
            if (entity is not null)
            {
                await _entities.AddAsync(entity);
                SaveChanges();
                return entity;
            }
            return null;
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
