using Microsoft.AspNetCore.Mvc;
using Via360.Api.Services;
using Via360.Shared.Models;

namespace Via360.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly FirestoreService _firestoreService;
        public ReportesController(FirestoreService firestoreService)
        {
            _firestoreService = firestoreService;
        }
        [HttpPost("crear")]
        public async Task<IActionResult> CrearReporte([FromBody] Reporte nuevoReporte)
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

            // Esto confirma que el objeto llegó y se deserializó correctamente
            Console.WriteLine($"[NUEVO REPORTE] Tipo: {nuevoReporte.Tipo}");
            Console.WriteLine($"[UBICACIÓN] Lat: {nuevoReporte.Ubicacion.Latitud}, Lon: {nuevoReporte.Ubicacion.Longitud}");
            Console.WriteLine($"[USUARIO] ID: {nuevoReporte.IdUsuario}");

            try
            {
                await _firestoreService.GuardarReporte(nuevoReporte);
                return Ok(new { mensaje = "Reporte vial creado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el reporte: {ex.Message}");
            }
        }
    }
}