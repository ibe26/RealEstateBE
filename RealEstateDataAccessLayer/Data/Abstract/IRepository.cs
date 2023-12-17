using System.Linq.Expressions;

namespace RealEstateDataAccessLayer.Data.Abstract
{
    public interface IRepository<TEntity> : IRepositoryReadWrite<TEntity> where TEntity : class
    {
        Task<bool> DeleteByIdAsync(int id);

        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Update(TEntity entity);

    }



}
