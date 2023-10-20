using Microsoft.AspNetCore.Mvc;

namespace DogsHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : Controller
    {
        public PingController()
        {
        }

        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Dogshouseservice.Version1.0.1");
        }
    }
}
