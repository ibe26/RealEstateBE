using RealEstateBE.Dal.Abstract;
using RealEstateBE.Entities;
using RealEstateBE.Entities.DTOs.User;
using RealEstateBE.Service.Abstract;
using System.Linq.Expressions;
using System.Security.Cryptography;

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
        public async Task<User?> GetUser(int id)
        {
            return id > 0 ? await _userDal.GetByIdAsync(id) : null;
        }

        public async Task<User?> Register(RegisterDTO registerDTO)
        {
            byte[] passwordHash, passwordKey;
            using (var hmac = new HMACSHA256())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDTO.Password));
            }
            var user = new User
            {
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Password = passwordHash,
                PasswordKey = passwordKey,
            };
            
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
