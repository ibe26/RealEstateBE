namespace RealEstateBE.Data.Abstract
{
    public interface IRepository<TEntity> : IRepositoryReadWrite<TEntity> where TEntity : class
    {
        Task<bool> DeleteByIdAsync(int id);
        Task<TEntity?> GetByIdAsync(int id);

        //Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<TEntity> Update(TEntity entity);

    }

    

}
