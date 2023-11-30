using RealEstateBE.Data.Abstract;
using RealEstateBE.Entities;
using System.Linq.Expressions;

namespace RealEstateBE.Dal.Abstract
{
    public interface IUserDal:IRepositoryReadWrite<User>
    {
        Task<bool> AnyUser(Expression<Func<User, bool>> predicate);
    }
}
