using Microsoft.AspNetCore.Mvc;

namespace AuthMiddlewarePlacement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("Seved by protected API");
        }
    }
}
