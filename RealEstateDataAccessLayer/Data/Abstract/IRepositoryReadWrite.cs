using System.Linq.Expressions;

namespace RealEstateDataAccessLayer.Data.Abstract
{
    public interface IRepositoryReadWrite<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);

        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> AddAsync(TEntity entity);

        int SaveChanges();
    }
}
