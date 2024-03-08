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

        [HttpGet(Routes.getList)]
        public async Task<IActionResult> getList()
        {
            return Ok(await _ownedPropertyService.GetOwnedProperties());
        }

        [HttpGet(Routes.getById)]
        public async Task<IActionResult> getById(int id)
        {
            return Ok(await _ownedPropertyService.GetOwnedProperty(id));
        }

        [HttpPost(Routes.insert)]
        public async Task<IActionResult> insertOwnedProperty([FromBody] OwnedPropertyDTO ownedPropertyDTO)
        {
            return Ok(await _ownedPropertyService.InsertOwnedProperty(ownedPropertyDTO));
        }

        [HttpDelete(Routes.deleteById)]
        public async Task<IActionResult> deleteProperty(int id)
        {
            return Ok(await _ownedPropertyService.DeleteOwnedProperty(id));
        }

        [HttpPut(Routes.update)]
        public async Task<IActionResult> updateProperty([FromBody] OwnedPropertyDTO ownedPropertyDTO, int id)
        {
            return Ok(await _ownedPropertyService.UpdateOwnedProperty(ownedPropertyDTO, id));
        }

    }
}
