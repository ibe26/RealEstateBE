using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateBE.Service.Abstract;
using RealEstateBE.Service.Concrete;

namespace RealEstateBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyControler : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyControler(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

    }
}
