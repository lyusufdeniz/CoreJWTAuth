using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;

namespace Auth.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(ResponseDTO<T> response) where T:class
        {
            return new ObjectResult(response) { StatusCode = response.StatusCode };

        }
    }
}
