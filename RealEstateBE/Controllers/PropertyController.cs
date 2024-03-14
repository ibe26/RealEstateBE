using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RealEstateBE.Controllers.Helper;
using RealEstateEntities.Entities.DTOs.Property;
using RealEstateService.Abstract;
using RealEstateEntities.Entities;
using Microsoft.AspNetCore.Authorization;
using RealEstateBE.Security;
using System;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ISecurity _security;
        private readonly IMemoryCache _memoryCache;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private const string PropertyCacheKey = "PropertyCacheKey";

        public PropertyController(IPropertyService propertyService,
                                  ISecurity security,
                                  IMemoryCache memoryCache,
                                  IWebHostEnvironment webHostEnvironment)
        {
            _propertyService = propertyService;
            _security= security;
            _memoryCache = memoryCache;
            _webHostEnvironment = webHostEnvironment;
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
        [HttpPost("{propertyGUID}")]
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

            if (token != null && property!=null)
            {
                if (!_security.IsAuthenticatedByToken(token,property.UserID))
                {
                    return Unauthorized();
                }
            }
            {
                //After the attempt of deletion; if SaveChanges()>0 returns true, return OK(). If not, return BadRequest()
                if (await _propertyService.DeleteProperty(propertyGUID))
                {
                    _memoryCache.Remove(PropertyCacheKey);
                    return Ok(propertyGUID);
                }
            }
            return BadRequest("Parameter is invalid.");
        }

        [HttpPost("image/upload/{propertyGUID}")]
        [Authorize]
        public IActionResult UploadImages(string propertyGUID)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = _propertyService.GetProperty(propertyGUID).Result;

            if (token != null && property != null)
            {
                if (!_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }
            var formFiles = Request.Form.Files;
            int succesfulUpload = 0;
            try
            {
                if (!Directory.Exists(GetFilePath(propertyGUID)))
                {
                    Directory.CreateDirectory(GetFilePath(propertyGUID));
                }
                foreach (var file in formFiles)
                {
                    string imagepath = GetFilePath(propertyGUID) + $@"\\{file.FileName}";
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        file.CopyTo(stream);
                        succesfulUpload++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (succesfulUpload - formFiles.Count != 0) ? BadRequest("Some files couldn't be uploaded.") : Ok($"{succesfulUpload} " + "Files Uploaded successfully");
        }

        [HttpGet(Routes.getById)]
        public IActionResult GetImages(string propertyGUID)
        {
            List<Photo> photoList = new();
            string hostUrl = $@"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            try
            {
                string filePath = GetFilePath(propertyGUID);
                if (Directory.Exists(filePath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string fileName = fileInfo.Name;
                        string imagePath = filePath + "\\" + fileName;
                        if (System.IO.File.Exists(imagePath))
                        {
                            string imageUrl = hostUrl + $@"{GetFilePath(propertyGUID)}+/{fileName}";
                            var photo = new Photo()
                            {
                                name = fileName,
                                url = imageUrl,
                            };
                            photoList.Add(photo);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(photoList);
        }

        [HttpDelete("delete")]
        [Authorize]
        public IActionResult DeleteImage(string propertyGUID, string imageName)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = _propertyService.GetProperty(propertyGUID).Result;

            if (token != null && property != null)
            {
                if (!_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }

            try
            {
                string imagepath = GetFilePath(propertyGUID) + $@"\\{imageName}";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                    if (!Directory.EnumerateFileSystemEntries(GetFilePath(propertyGUID)).Any())
                    {
                        Directory.Delete(GetFilePath(propertyGUID));
                    }
                    return Ok(propertyGUID);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return BadRequest("Such image does not exist.");
        }
        [NonAction]
        private string GetFilePath(string propertyGUID)
        {
            return this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property\\{propertyGUID}";
        }
    }
}

