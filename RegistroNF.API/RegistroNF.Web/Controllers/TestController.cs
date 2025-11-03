using Microsoft.AspNetCore.Mvc;

namespace RegistroNF.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is working!");
        }
    }
}
