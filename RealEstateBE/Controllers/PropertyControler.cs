using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using RealEstateBE.Controllers.Helper;
using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;
using RealEstateBE.Service.Abstract;
using System.IO;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IMemoryCache _memoryCache;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private const string ProductCacheKey = "ProductCacheKey";

        public PropertyController(IPropertyService propertyService,
                                  IMemoryCache memoryCache,
                                  IWebHostEnvironment webHostEnvironment)
        {
            _propertyService = propertyService;
            _memoryCache = memoryCache;
            _webHostEnvironment = webHostEnvironment;
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

                var property = await _propertyService.InsertProperty(propertyDTO);
                if (property != null)
                {
                    _memoryCache.Remove(ProductCacheKey);
                    return Ok(property);
                }
                return BadRequest("Couldn't insert given property.");
            }
            return BadRequest("Please provide a valid body.");
        }

        [HttpPost(Routes.update)]
        public IActionResult UpdateProperty([FromBody] PropertyDTO propertyDTO, int id)
        {
            if (propertyDTO != null && id > 0)
            {
                var property = _propertyService.UpdateProperty(propertyDTO!, id);
                if (property != null)
                {
                    _memoryCache.Remove(ProductCacheKey);
                    return Ok(property);
                }
            }
            return BadRequest("Parameter is invalid.");
        }
        [HttpDelete(Routes.deleteById)]
        public async Task<IActionResult> DeleteProperty(int id)
        {
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

        [HttpPost(Routes.uploadImages)]
        public async Task<IActionResult> UploadImage(IFormFileCollection formFiles, int propertyID)
        {
            int succesfulUpload = 0;
            try
            {
                string filePath = this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property{propertyID}";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                foreach (var file in formFiles)
                {
                    string imagepath = filePath + $@"\\{file.FileName}";
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        succesfulUpload++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return (succesfulUpload - formFiles.Count != 0) ? BadRequest("Some files couldn't be uploaded.") : Ok("Files Uploaded successfully.");
        }

        [HttpPost(Routes.getImages)]
        public async Task<IActionResult> GetImages(int propertyID)
        {
            List<string> imageUrList=new ();
            string hostUrl = $@"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            try
            {
                string filePath = GetFilePath(propertyID);
                if(Directory.Exists(filePath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach(FileInfo fileInfo in fileInfos)
                    {
                        string fileName=fileInfo.Name;
                        string imagePath = filePath + "\\" + fileName;
                        if (System.IO.File.Exists(imagePath))
                        {
                            string imageUrl = hostUrl + $@"/Upload/Property{propertyID}/{fileName}";
                            imageUrList.Add(imageUrl);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(imageUrList);
        }
        [NonAction]
        private string GetFilePath(int propertyID)
        {
            return this._webHostEnvironment.WebRootPath + $@"\\Upload\\Property{propertyID}";
        }
    }
}
