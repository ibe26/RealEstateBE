using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet(Helper.Routes.getList)]
        public async Task<IActionResult> PropertyList()
        {
            return Ok(await _propertyService.GetProperties());
        }

        [HttpPost(Helper.Routes.filterList)]
        public async Task<IActionResult> FilterProperties(PropertyFilterDTO propertyFilterDTO)
        {
            return Ok(await _propertyService.FilterPropertiesAsync(propertyFilterDTO));
        }

        [HttpDelete(Helper.Routes.deleteById)]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            if (id > 0)
            {
                return Ok(await _propertyService.DeleteProperty(id));
            }
            return BadRequest(id);
        }

        [HttpPost(Helper.Routes.getById)]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            if (id > 0)
            {
                return Ok(await _propertyService.GetProperty(id));
            }
            return BadRequest(id);
        }

        [HttpPost(Helper.Routes.insert)]
        public async Task<IActionResult> InsertProperty(PropertyDTO propertyDTO)
        {
            if (propertyDTO != null)
            {
                return Ok(await _propertyService.InsertProperty(propertyDTO));
            }
            return BadRequest();
        }

        [HttpPost(Helper.Routes.update)]
        public async Task<IActionResult> UpdateProperty([FromBody] PropertyDTO propertyDTO, int id)
        {
            if(propertyDTO != null  || id > 0)
            {
                return Ok(await _propertyService.UpdateProperty(propertyDTO!, id));
            }
            return BadRequest();
        }
    }
}
