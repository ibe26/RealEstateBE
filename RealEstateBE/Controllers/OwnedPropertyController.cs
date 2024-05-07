using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateBE.Controllers.Helper;
using RealEstateBE.Security;
using RealEstateControllerLayer.Controllers.Helper;
using RealEstateEntities.Entities;
using RealEstateEntities.Entities.DTOs.Property;
using RealEstateServiceLayer.Abstract;

namespace RealEstateControllerLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnedPropertyController : ControllerBase
    {
        private readonly IOwnedPropertyService _ownedPropertyService;
        private readonly IImageOperations _imageOperations;
        private readonly ISecurity _security;

        private readonly string category = "OwnedProperty";
        public OwnedPropertyController(IOwnedPropertyService ownedPropertyService,
                                       IImageOperations imageOperations,
                                       ISecurity security)
        {
            _ownedPropertyService = ownedPropertyService;
            _imageOperations = imageOperations;
            _security = security;
        }

        [HttpGet("user/{userGUID}")]
        public async Task<IActionResult> getList(string userGUID)
        {
            return Ok(await _ownedPropertyService.GetOwnedProperties(userGUID));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            return Ok(await _ownedPropertyService.GetOwnedProperty(id));
        }

        [HttpPost()]
        public async Task<IActionResult> insertOwnedProperty([FromBody] OwnedPropertyDTO ownedPropertyDTO)
        {
            return Ok(await _ownedPropertyService.InsertOwnedProperty(ownedPropertyDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteProperty(int id)
        {
            return Ok(await _ownedPropertyService.DeleteOwnedProperty(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateProperty([FromBody] OwnedPropertyDTO ownedPropertyDTO, int id)
        {
            return Ok(await _ownedPropertyService.UpdateOwnedProperty(ownedPropertyDTO, id));
        }

        [HttpPost("Image/{id}")]
        [Authorize]
        public async Task<IActionResult> UploadImages(int id)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = await _ownedPropertyService.GetOwnedProperty(id);

            if (token != null && property != null)
            {
                if (!_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }
            var formFile = Request.Form.Files[0];
            _imageOperations.UploadImageSingle(id.ToString(),category, formFile);

            return Ok();
        }

        [HttpGet("Image/{propertyID}")]
        public IActionResult GetImages(int propertyID)
        {
            IList<Photo> photoList = new List<Photo>();
            string hostUrl = $@"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            _imageOperations.GetPhotos(propertyID.ToString(),category, hostUrl, photoList);
            return Ok(photoList);
        }

        [HttpDelete("Image")]
        [Authorize]
        public async Task<IActionResult> DeleteImage(int propertyID, string imageName)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var property = await _ownedPropertyService.GetOwnedProperty(propertyID);

            if (token != null && property != null)
            {
                if (!_security.IsAuthenticatedByToken(token, property.UserID))
                {
                    return Unauthorized();
                }
            }

            _imageOperations.DeleteImages(propertyID.ToString(),category, imageName);
            return Ok();
        }
    }
}
