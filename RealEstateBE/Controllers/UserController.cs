using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateEntities.Entities.DTOs.User;
using RealEstateEntities.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using RealEstateService.Abstract;
using RealEstateBE.Controllers.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Principal;
using System.Text;

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
            this._configuration = configuration;
        }

        [HttpGet(Routes.getList)]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetUsers();

            return Ok(users.Select(u => new 
            {
                u.UserID,
                u.Email,
                u.FirstName,
                u.LastName,
                u.ListedProperties,
            }));
        }
        [HttpGet(Routes.getById)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user != null)
            {
                return Ok(new
                {
                    user.UserID,
                    user.ListedProperties,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                });
            }
            return BadRequest("User is not found.");
        }

        [HttpPost(Routes.register)]
        public async Task<IActionResult> Register(RegisterDTO registerUser)
        {
            if (await _userService.AnyUser(u => u.Email == registerUser.Email))
            {
                return BadRequest("User Alrady Exists.");
            }

            return Ok(await _userService.Register(registerUser));
        }

        [HttpPost(Routes.login)]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            //First check if user exists. If not, break.
            //Then Check if User Token exists. If exists,Check passwords. If doesn't match, break. If matches, return current token.
            //If User Token doesn't exists, check for password validation.If matches, create new token and return the current.


            User? FoundUser = await _userService.FilterUserSingle(u => u.Email == loginDTO.Email);
            if (FoundUser is null)
            {
                return BadRequest("User doesn't exist.");
            }
            if (await _userService.AnyUser(u => u.UserID == FoundUser.UserID))
            {
                if (!MatchPassword(loginDTO.Password, FoundUser.Password, FoundUser.PasswordKey))
                {
                    return BadRequest("Please Check e-mail or password entered.");
                }
            }
            string Token = CreateJWTAsync(loginDTO);
            if (Token == null)
            {
                return BadRequest();
            }
            return Ok(new UserDTO
            {
                UserID=FoundUser.UserID,
                Email = FoundUser.Email,
                FirstName = FoundUser.FirstName,
                LastName = FoundUser.LastName,
                Properties=FoundUser.ListedProperties,
                Token = Token
            });

        }
        [NonAction]
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

        [NonAction]
        private string CreateJWTAsync(LoginDTO loginDTO)
        {
            var user=_userService.FilterUserSingle(u=>u.Email == loginDTO.Email).Result;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email,loginDTO.Email),
                new Claim("ID",user!.UserID.ToString())
            };

            var signingCredantials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience= _configuration["JwtSettings:Audience"],
                Issuer= _configuration["JwtSettings:Issuer"],
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = signingCredantials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        [NonAction]
        private bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters  = new TokenValidationParameters
            {
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidAudience = _configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        [HttpGet("validateToken")]
        public IActionResult validateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var securityToken = handler.ReadJwtToken(token);
                if (ValidateToken(token) && securityToken != null)
                {
                    return Ok(securityToken.Claims.SingleOrDefault(c => c.Type.Equals("ID")).Value);
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return BadRequest();
        }

    }
}

