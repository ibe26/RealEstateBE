using Microsoft.EntityFrameworkCore;
using RealEstateDataAccessLayer.Abstract;
using RealEstateDataAccessLayer.Data.Concrete;
using RealEstateEntities.Entities;
using RealEstateEntities.Entities.DTOs.User;
using System.Linq.Expressions;

namespace RealEstateDataAccessLayer.Concrete
{
    public class UserDal : RepositoryReadWrite<User>, IUserDal
    {
        public async Task<bool> AnyUser(Expression<Func<User, bool>> predicate)
        {
           return await base._entities.AnyAsync(predicate);
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await base._entities.ToListAsync();
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await base._entities.Include(u=>u.Properties).SingleOrDefaultAsync(u=>u.UserID==id);
        }

    }
}
