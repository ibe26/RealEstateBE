using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using RealEstateBE.Controllers.Helper;
using RealEstateBE.Entities.DTOs.Property;
using RealEstateBE.Service.Abstract;
using RealEstateBE.Entities;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using RealEstateBE.Security;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ISecurity _security;
        private readonly IMemoryCache _memoryCache;

        private const string ProductCacheKey = "ProductCacheKey";

        public PropertyController(IPropertyService propertyService,
                                  ISecurity security,
                                  IMemoryCache memoryCache)
        {
            _propertyService = propertyService;
            _security= security;
            _memoryCache = memoryCache;
        }
        [HttpGet(Routes.getList)]
        public async Task<IActionResult> PropertyList()
        {
            IEnumerable<Property>? properties = _memoryCache.Get<IEnumerable<Property>>(ProductCacheKey);
            if (properties != null)
            {
                return Ok(properties);
            }

            return Ok(_memoryCache.Set(ProductCacheKey, await _propertyService.GetProperties()));
        }

        [HttpGet(Routes.getById)]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            Property? property = await _propertyService.GetProperty(id);
            if (property != null)
            {
                return Ok(property);
            }
            return BadRequest("Parameter is invalid.");
        }

        [HttpPost(Routes.filterList)]
        public async Task<IActionResult> FilterProperties(PropertyFilterDTO propertyFilterDTO)
        {
            return Ok(await _propertyService.FilterPropertiesAsync(propertyFilterDTO));
        }

        [HttpPost(Routes.insert)]
        public async Task<IActionResult> InsertProperty(PropertyDTO propertyDTO)
        {
            //if body is null, return BadRequest()

            if (propertyDTO != null)
            {
                //We should be getting SaveChanges()>0 as true, only then return Ok() 200. If not, return BadRequest.

                var _property = await _propertyService.InsertProperty(propertyDTO);
                if (_property != null)
                {
                    Property? property = await _propertyService.GetProperty(_property.PropertyID);
                    _memoryCache.Remove(ProductCacheKey);
                    return Ok(property);
                }
                return BadRequest("Couldn't insert given property.");
            }
            return BadRequest("Please provide a valid body.");
        }

        [Authorize]
        [HttpPost(Routes.update)]
        public async Task<IActionResult> UpdateProperty([FromBody] PropertyDTO propertyDTO, int id)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = await _propertyService.GetProperty(id);

            if (token != null && property != null)
            {
                if (_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }

            if (propertyDTO != null && id > 0)
            {
                property = await _propertyService.UpdateProperty(propertyDTO!, id);
                if (property != null)
                {
                    _memoryCache.Remove(ProductCacheKey);
                    return Ok(property);
                }
            }
            return BadRequest("Parameter is invalid.");
        }

        [Authorize]
        [HttpDelete(Routes.deleteById)]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = await _propertyService.GetProperty(id);

            if (token != null && property!=null)
            {
                if (!_security.IsAuthenticatedByToken(token,property.UserID))
                {
                    return Unauthorized();
                }
            }
            //id should be valid
            if (id > 0)
            {
                //After the attempt of deletion; if SaveChanges()>0 returns true, return OK(). If not, return BadRequest()
                if (await _propertyService.DeleteProperty(id))
                {
                    _memoryCache.Remove(ProductCacheKey);
                    return Ok(id);
                }
            }
            return BadRequest("Parameter is invalid.");
        }

    }
}
