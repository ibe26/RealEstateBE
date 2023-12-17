using RealEstateDataAccessLayer.Data.Abstract;
using RealEstateEntities.Entities;
using System.Linq.Expressions;

namespace RealEstateDataAccessLayer.Abstract
{
    public interface IUserDal:IRepositoryReadWrite<User>
    {
        Task<bool> AnyUser(Expression<Func<User, bool>> predicate);
    }
}
