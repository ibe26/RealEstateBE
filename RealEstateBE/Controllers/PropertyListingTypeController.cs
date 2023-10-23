using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateBE.Controllers.Helper;
using RealEstateBE.Entities.DTOs;
using RealEstateBE.Model;
using RealEstateBE.Service.Abstract;
using RealEstateBE.Service.Concrete;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyListingTypeController : ControllerBase
    {
        private readonly IPropertyListingTypeService _propertyListingTypeService;

        public PropertyListingTypeController(IPropertyListingTypeService propertyListingTypeService)
        {
            _propertyListingTypeService = propertyListingTypeService;
        }

        [HttpGet(Routes.getList)]
        public async Task<IActionResult> propertyListingTypes()
        {
            return Ok(await _propertyListingTypeService.GetPropertyListingTypes());
        }

        [HttpPost(Routes.insert)]
        public async Task<IActionResult> Insert(PropertyListingTypeDTO propertyListingTypeDTO)
        {
            if(propertyListingTypeDTO != null)
            {
                if(await _propertyListingTypeService.InsertPropertyListingType(propertyListingTypeDTO))
                {
                    return Ok(propertyListingTypeDTO);
                }
            }
            return BadRequest("Given parameter is invalid.");
        }
    }
}
