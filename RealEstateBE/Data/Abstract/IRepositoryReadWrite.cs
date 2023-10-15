using System.Linq.Expressions;

namespace RealEstateBE.Data.Abstract
{
    public interface IRepositoryReadWrite<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AddAsync(TEntity entity);

        bool SaveChanges();
    }
}
