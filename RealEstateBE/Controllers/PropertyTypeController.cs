using Microsoft.AspNetCore.Mvc;
using RealEstateBE.Controllers.Helper;
using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;
using RealEstateBE.Service.Abstract;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypeController : ControllerBase
    {
        private readonly IPropertyTypeService _propertyTypeService;
        public PropertyTypeController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        //GET /api/PropertyType/getList
        [HttpGet(Routes.getList)]
        public async Task<IActionResult> PropertyTypeList()
        {
            return Ok( await _propertyTypeService.GetPropertyTypes());
        }

        //POST /api/PropertyType/insert
        [HttpPost(Routes.insert)]
        public async Task<IActionResult> InsertProperty(PropertyTypeDTO property)
        {
            if (property != null)
            {
               return Ok(await _propertyTypeService.InsertPropertyType(property));
            }
            return BadRequest();
        }
    }
}
