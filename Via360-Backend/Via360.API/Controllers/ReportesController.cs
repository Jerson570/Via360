using Microsoft.AspNetCore.Mvc;
using Via360.Shared.Models;

namespace Via360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        // endpoint de Swagger: POST /api/reportes
        [HttpPost]
        public IActionResult RecibirReporte([FromBody] Reporte nuevoReporte)
        {
            // 1. Validación de Seguridad (Garantía de que no llegue basura)
            if (nuevoReporte == null)
            {
                return BadRequest("El reporte no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(nuevoReporte.Descripcion))
            {
                return BadRequest("La descripción es obligatoria.");
            }

            // 2. Validación de Ubicación
            if (nuevoReporte.Ubicacion == null || (nuevoReporte.Ubicacion.Latitud == 0 && nuevoReporte.Ubicacion.Longitud == 0))
            {
                return BadRequest("El reporte debe incluir coordenadas de GPS válidas.");
            }

            // 3. Simulación de Proceso (Log en consola)
            // Esto confirma que el objeto llegó y se deserializó correctamente
            Console.WriteLine($"[NUEVO REPORTE] Tipo: {nuevoReporte.Tipo}");
            Console.WriteLine($"[UBICACIÓN] Lat: {nuevoReporte.Ubicacion.Latitud}, Lon: {nuevoReporte.Ubicacion.Longitud}");
            Console.WriteLine($"[USUARIO] ID: {nuevoReporte.IdUsuario}");

            // 4. Respuesta de éxito
            return Ok(new
            {
                Mensaje = "Reporte validado y recibido por el Backend",
                IdGenerado = nuevoReporte.IdReporte,
                FechaRecibido = DateTime.Now
            });
        }
    }
}