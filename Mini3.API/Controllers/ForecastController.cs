using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mini3.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Güneşli");
        }
    }
}
