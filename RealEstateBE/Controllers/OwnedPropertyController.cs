using Microsoft.AspNetCore.Mvc;
using RealEstateBE.Controllers.Helper;
using RealEstateEntities.Entities.DTOs.Property;
using RealEstateServiceLayer.Abstract;

namespace RealEstateControllerLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnedPropertyController : ControllerBase
    {
        private readonly IOwnedPropertyService _ownedPropertyService;

        public OwnedPropertyController(IOwnedPropertyService ownedPropertyService)
        {
            _ownedPropertyService = ownedPropertyService;
        }

        [HttpGet()]
        public async Task<IActionResult> getList()
        {
            return Ok(await _ownedPropertyService.GetOwnedProperties());
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

        [HttpDelete("id")]
        public async Task<IActionResult> deleteProperty(int id)
        {
            return Ok(await _ownedPropertyService.DeleteOwnedProperty(id));
        }

        [HttpPut("id")]
        public async Task<IActionResult> updateProperty([FromBody] OwnedPropertyDTO ownedPropertyDTO, int id)
        {
            return Ok(await _ownedPropertyService.UpdateOwnedProperty(ownedPropertyDTO, id));
        }

    }
}
