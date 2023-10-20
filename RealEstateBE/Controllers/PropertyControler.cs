using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;
using RealEstateBE.Service.Abstract;
using RealEstateBE.Service.Concrete;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyListingTypeService _propertyTypeListService;
        private readonly IMemoryCache _memoryCache;

        private const string ProductCacheKey = "ProductCacheKey";

        public PropertyController(IPropertyService propertyService,
                                  IPropertyTypeService propertyTypeService,
                                  IPropertyListingTypeService propertyListingTypeService,
                                  IMemoryCache memoryCache)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _propertyTypeListService = propertyListingTypeService;
            _memoryCache = memoryCache;
        }

        [HttpGet(Helper.Routes.getList)]
        public async Task<IActionResult> PropertyList()
        {
            IEnumerable<Property>? properties = _memoryCache.Get<IEnumerable<Property>>(ProductCacheKey);
            if(properties != null)
            {
                return Ok(properties);
            }

           return Ok(_memoryCache.Set(ProductCacheKey, await _propertyService.GetProperties()));
        }

        [HttpGet(Helper.Routes.getById)]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            if (id > 0)
            {
                if ((await _propertyService.GetProperty(id)) != null)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost(Helper.Routes.filterList)]
        public async Task<IActionResult> FilterProperties(PropertyFilterDTO propertyFilterDTO)
        {
            return Ok(await _propertyService.FilterPropertiesAsync(propertyFilterDTO));
        }

        [HttpPost(Helper.Routes.insert)]
        public async Task<IActionResult> InsertProperty(PropertyDTO propertyDTO)
        {
            //if body is null, return BadRequest()

            if (propertyDTO != null)
            {
                //In body PropertyTypeID and PropertyListingTypeID should be valid and exist in according to their database table.
                //So we must check whether these ID's exist in database or not. If not, return BadRequest.

                if ((await _propertyTypeService.GetPropertyTypes()).Any(p => p.PropertyTypeID == propertyDTO.PropertyTypeID) &&
                    (await _propertyTypeListService.GetPropertyListingTypes()).Any(p => p.PropertyListingTypeID == propertyDTO.PropertyListingTypeID))
                {
                    //We should be getting SaveChanges()>0 as true, only then return Ok() 200. If not, return BadRequest.

                    if (await _propertyService.InsertProperty(propertyDTO))
                    {
                        _memoryCache.Remove(ProductCacheKey);
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost(Helper.Routes.update)]
        public async Task<IActionResult> UpdateProperty([FromBody] PropertyDTO propertyDTO, int id)
        {
            if (propertyDTO != null || id > 0)
            {
                if (await _propertyService.UpdateProperty(propertyDTO!, id))
                {
                    _memoryCache.Remove(ProductCacheKey);
                    return Ok();
                }
            }
            return BadRequest();
        }
        [HttpDelete(Helper.Routes.deleteById)]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            //id should be valid
            if (id > 0)
            {
                //After the attempt of deletion; if SaveChanges()>0 returns true, return OK(). If not, return BadRequest()
                if(await _propertyService.DeleteProperty(id))
                {
                    _memoryCache.Remove(ProductCacheKey);
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
