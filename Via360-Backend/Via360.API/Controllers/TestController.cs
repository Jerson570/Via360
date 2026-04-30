using Microsoft.AspNetCore.Mvc;
using Via360.Shared.Models; // Aquí es donde se conectan

namespace Via360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var miReporte = new Reporte
            {
                Id = "1",
                Descripcion = "Hueco en la vía principal"
            };
            return Ok(miReporte);
        }
    }
}