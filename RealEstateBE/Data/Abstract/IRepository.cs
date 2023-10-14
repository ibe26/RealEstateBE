using System.Linq.Expressions;

namespace RealEstateBE.Data.Abstract
{
    public interface IRepository<TEntity> where TEntity:class
    {

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<bool> AddAsync(TEntity entity);
        //Task<TEntity> Update(TEntity entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        bool SaveChanges();

    }

}
