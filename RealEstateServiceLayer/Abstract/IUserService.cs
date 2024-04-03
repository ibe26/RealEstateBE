using RealEstateEntities.Entities;
using RealEstateEntities.Entities.DTOs.User;
using System.Linq.Expressions;

namespace RealEstateService.Abstract
{
    public interface IUserService
    {
        public Task<IEnumerable<User?>> GetUsers();
        public Task<User?> GetUser(string id);
        public Task<User?> Register(RegisterDTO register);
        public Task<UserDTO?> Login(LoginDTO loginDTO);
        public Task<User?> FilterUserSingle(Expression<Func<User,bool>> expression);
        public Task<bool> AnyUser(Expression<Func<User,bool>> expression);
    }
}
