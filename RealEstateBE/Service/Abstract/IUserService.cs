using RealEstateBE.Entities;
using RealEstateBE.Entities.DTOs.User;
using System.Linq.Expressions;

namespace RealEstateBE.Service.Abstract
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User?> GetUser(int id);
        public Task<User?> Register(User user);
        public Task<User?> FilterUserSingle(Expression<Func<User,bool>> expression);
        public Task<bool> AnyUser(Expression<Func<User,bool>> expression);
    }
}
