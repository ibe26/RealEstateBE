using Microsoft.IdentityModel.Tokens;
using RealEstateDataAccessLayer.Abstract;
using RealEstateEntities.Entities;
using RealEstateEntities.Entities.DTOs.User;
using RealEstateService.Abstract;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RealEstateService.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;

        public UserService(IUserDal userDal)
        {
            this._userDal = userDal;
        }
        public async Task<IEnumerable<User?>> GetUsers()
        {
            return await _userDal.GetAllAsync();
        }
        public async Task<User?> GetUser(string id)
        {
            Guid guid=new Guid(id);
            return await _userDal.GetByIdAsync(guid);
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
            return await _userDal.Any(expression);
        }

        public async Task<UserDTO?> Login(LoginDTO loginDTO)
        {
            User? FoundUser = await _userDal.SingleOrDefaultAsync(u => u.Email == loginDTO.Email);
            if (FoundUser is null)
            {
                return null;
            }
            if (await _userDal.Any(u => u.UserID == FoundUser.UserID))
            {
                if (!MatchPassword(loginDTO.Password, FoundUser.Password, FoundUser.PasswordKey))
                {
                    return null;
                }
            }
            string Token = CreateJWT(loginDTO);
            if(Token == null)
            {
                return null;
            }
            return new UserDTO
            {
                UserID= FoundUser.UserID,
                Email = FoundUser.Email,
                FirstName = FoundUser.FirstName,
                LastName = FoundUser.LastName,
                Properties=FoundUser.ListedProperties,
                OwnedProperties=FoundUser.OwnedProperties,
                Token = Token
            };
        }
        private bool MatchPassword(string passwordText, byte[] UserPaswword, byte[] PasswordKey)
        {
            using (var hmac = new HMACSHA256(PasswordKey))
            {
                var passwordKey = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != UserPaswword[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private string CreateJWT(LoginDTO loginDTO)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(loginDTO.Password + "TOKENTEST123123123123123"));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email,loginDTO.Email)
            };

            var signingCredantials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = signingCredantials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
