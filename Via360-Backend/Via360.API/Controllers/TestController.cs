using Microsoft.AspNetCore.Mvc;

namespace Via360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("API funcionando con controladores.");
    }
}