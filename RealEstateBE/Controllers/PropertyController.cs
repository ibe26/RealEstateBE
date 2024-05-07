using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RealEstateBE.Controllers.Helper;
using RealEstateEntities.Entities.DTOs.Property;
using RealEstateService.Abstract;
using RealEstateEntities.Entities;
using Microsoft.AspNetCore.Authorization;
using RealEstateBE.Security;
using System;
using RealEstateControllerLayer.Controllers.Helper;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ISecurity _security;
        private readonly IMemoryCache _memoryCache;
        private readonly IImageOperations _imageOperations;

        private readonly string category = "Property";

        private const string PropertyCacheKey = "PropertyCacheKey";

        public PropertyController(IPropertyService propertyService,
                                  ISecurity security,
                                  IMemoryCache memoryCache,
                                  IImageOperations imageOperations)
        {
            _propertyService = propertyService;
            _security = security;
            _memoryCache = memoryCache;
            _imageOperations = imageOperations;
        }

        [HttpGet()]
        public async Task<IActionResult> PropertyList()
        {
            IEnumerable<Property>? properties = _memoryCache.Get<IEnumerable<Property>>(PropertyCacheKey);
            if (properties != null)
            {
                return Ok(properties);
            }

            return Ok(_memoryCache.Set(PropertyCacheKey, await _propertyService.GetProperties()));
        }

        [HttpGet("{propertyGUID}")]
        public async Task<IActionResult> GetPropertyById(string propertyGUID)
        {
            Property? property = await _propertyService.GetProperty(propertyGUID);
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

        [HttpPost()]
        public async Task<IActionResult> InsertProperty(PropertyDTO propertyDTO)
        {
            //if body is null, return BadRequest()

            if (propertyDTO != null)
            {
                //We should be getting SaveChanges()>0 as true, only then return Ok() 200. If not, return BadRequest.

                var _property = await _propertyService.InsertProperty(propertyDTO);
                if (_property != null)
                {
                    _memoryCache.Remove(PropertyCacheKey);
                    return Ok(_property);
                }
                return BadRequest("Couldn't insert given property.");
            }
            return BadRequest("Please provide a valid body.");
        }

        [Authorize]
        [HttpPut("{propertyGUID}")]
        public async Task<IActionResult> UpdateProperty([FromBody] PropertyDTO propertyDTO, string propertyGUID)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = await _propertyService.GetProperty(propertyGUID);

            if (token != null && property != null)
            {
                if (!_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }

            if (propertyDTO != null)
            {
                property = await _propertyService.UpdateProperty(propertyDTO!, propertyGUID);
                if (property != null)
                {
                    _memoryCache.Remove(PropertyCacheKey);
                    return Ok(property);
                }
            }
            return BadRequest("Parameter is invalid.");
        }

        [Authorize]
        [HttpDelete("{propertyGUID}")]
        public async Task<IActionResult> DeleteProperty(string propertyGUID)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = await _propertyService.GetProperty(propertyGUID);

            if (token != null && property != null)
            {
                if (!_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }
            {
                //After the attempt of deletion; if SaveChanges()>0 returns true, return OK(). If not, return BadRequest()
                if (await _propertyService.DeleteProperty(propertyGUID))
                {
                    _memoryCache.Remove(PropertyCacheKey);
                    return Ok(new JsonResult(propertyGUID));
                }
            }
            return BadRequest("Parameter is invalid.");
        }

        [HttpPost("Image/{propertyGUID}")]
        [Authorize]
        public async Task<IActionResult> UploadImages(string propertyGUID)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = await _propertyService.GetProperty(propertyGUID);

            if (token != null && property != null)
            {
                if (!_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }
            var formFiles = Request.Form.Files;
            int succesfulUpload;
            _imageOperations.UploadImages(propertyGUID,category, formFiles, out succesfulUpload);

            return (succesfulUpload - formFiles.Count != 0) ? BadRequest("Some files couldn't be uploaded.") : Ok(JsonContent.Create($"{succesfulUpload} " + "Files Uploaded successfully"));
        }

        [HttpGet("Image/{propertyGUID}")]
        public IActionResult GetImages(string propertyGUID)
        {
            IList<Photo> photoList = new List<Photo>();
            string hostUrl = $@"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            _imageOperations.GetPhotos(propertyGUID,category, hostUrl, photoList);
            return Ok(photoList);
        }

        [HttpDelete("Image")]
        [Authorize]
        public async Task<IActionResult> DeleteImage(string propertyGUID, string imageName)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = await _propertyService.GetProperty(propertyGUID);

            if (token != null && property != null)
            {
                if (!_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }

            _imageOperations.DeleteImages(propertyGUID.ToString(),category, imageName);
            
            return Ok();
        }
    }
}

