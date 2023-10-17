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
        public async Task<IEnumerable<PropertyListingType>> propertyListingTypes()
        {
            return await _propertyListingTypeService.GetPropertyListingTypes();
        }

        [HttpPost(Routes.insert)]
        public async Task<bool> Insert(PropertyListingTypeDTO propertyListingTypeDTO)
        {
            return await _propertyListingTypeService.InsertPropertyListingType(propertyListingTypeDTO);
        }
    }
}
