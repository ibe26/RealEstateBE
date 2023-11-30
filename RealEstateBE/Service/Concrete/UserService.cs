using RealEstateBE.Dal.Abstract;
using RealEstateBE.Entities;
using RealEstateBE.Entities.DTOs.User;
using RealEstateBE.Service.Abstract;
using System.Linq.Expressions;

namespace RealEstateBE.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            this._userDal = userDal;
        }
        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
        public Task<User?> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> Register(User user)
        {
            return await _userDal.AddAsync(user);
        }
        public async Task<User?> FilterUserSingle(Expression<Func<User,bool>> expression)
        {
            return await _userDal.SingleOrDefaultAsync(expression);
        }

        public async Task<bool> AnyUser(Expression<Func<User, bool>> expression)
        {
            return await _userDal.AnyUser(expression);
        }

    }
}
