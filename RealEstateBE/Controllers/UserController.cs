using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RealEstateBE.Entities.DTOs.User;
using RealEstateBE.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using RealEstateBE.Service.Abstract;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService,
                              IConfiguration configuration)
        {
            this._userService = userService;
            this._configuration= configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            
        }

        [HttpGet("{UserID}")]
        public async Task<IActionResult> UserWithPosts(int UserID)
        {
            
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerUser)
        {
            if (await uow.UserRepository.AnyAsync(u => u.Nickname == registerUser.NickName || u.Email == registerUser.Email))
            {
                return BadRequest("User Alrady Exists.");
            }
            byte[] passwordHash, passwordKey;
            using (var hmac = new HMACSHA256())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerUser.Password));
            }

            var user = new User
            {
                Email = registerUser.Email,
                Password = passwordHash,
                PasswordKey = passwordKey,
            };
            await uow.UserRepository.InsertAsync(user);
            await uow.SaveAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            //First check if user exists. If not, break.
            //Then Check if User Token exists. If exists,Check passwords. If doesn't match, break. If matches, return current token.
            //If User Token doesn't exists, check for password validation.If matches, create new token and return the current.

            User FoundUser = await uow.UserRepository.SingleOrDefault(u => u.Email == user.Email);
            if (FoundUser is null)
            {
                return BadRequest("User doesn't exist.");
            }
            if (await uow.UserRepository.AnyAsync(u => u.UserID == FoundUser.UserID))
            {
                if (!MatchPassword(user.Password, FoundUser.Password, FoundUser.PasswordKey))
                {
                    return BadRequest("Please Check e-mail or password entered.");
                }
            }
            string Token = CreateJWT(user);
            if (Token == null)
            {
                return BadRequest();
            }
            return Ok(new { nickname = FoundUser.Nickname, Token = Token });

        }

        [NonAction]
        private string CreateJWT(LoginDTO user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(user.Password + _configuration["JwtSettings:Key"]!));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email,user.Email)
            };

            var signingCredantials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = signingCredantials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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


    }
}

