using Microsoft.EntityFrameworkCore;
using RealEstateBE.Data.Abstract;
using System.Linq.Expressions;

namespace RealEstateBE.Data.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static readonly DataContext _context =new ();
        private readonly DbSet<TEntity> _entities=_context.Set<TEntity>();

        //public Repository(DataContext context)
        //{
        //    _context = context;
        //    _entities = _context.Set<TEntity>();
        //}
        public async virtual Task AddAsync(TEntity entity)
        {
            if (entity is not null)
            {
               await _entities.AddAsync(entity);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id > 0)
            {
                TEntity? t = await _entities.FindAsync(id);
                if (t != null)
                {
                   _context.Remove(t);
                }
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            if(id > 0)
            {
                return await _entities.FindAsync(id);
            }
            return null;
        }

        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        //public TEntity Update(TEntity entity)
        //{
        //    _entities.Update(entity);
        //    _context.SaveChanges();
        //    return entity;
        //}

        public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges()>0;
        }
    }
}
