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

        [HttpPost(Helper.Routes.filterList)]
        public async Task<IEnumerable<Property>> FilterProperties(PropertyFilterDTO propertyFilterDTO)
        {
            return await _propertyService.FilterPropertiesAsync(propertyFilterDTO);
        }

    }
}
