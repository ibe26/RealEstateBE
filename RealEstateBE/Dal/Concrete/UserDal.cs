using Microsoft.EntityFrameworkCore;
using RealEstateBE.Dal.Abstract;
using RealEstateBE.Data.Concrete;
using RealEstateBE.Entities;
using System.Linq.Expressions;

namespace RealEstateBE.Dal.Concrete
{
    public class UserDal : RepositoryReadWrite<User>, IUserDal
    {
        public async Task<bool> AnyUser(Expression<Func<User, bool>> predicate)
        {
           return await base._entities.AnyAsync(predicate);
        }
    }
}
