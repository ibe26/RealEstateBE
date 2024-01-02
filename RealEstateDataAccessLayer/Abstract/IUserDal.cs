using RealEstateDataAccessLayer.Data.Abstract;
using RealEstateEntities.Entities;
using System.Linq.Expressions;

namespace RealEstateDataAccessLayer.Abstract
{
    public interface IUserDal : IRepositoryReadWrite<User>
    {
        Task<bool> Any(Expression<Func<User, bool>> predicate);
    }
}
