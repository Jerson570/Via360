using Microsoft.AspNetCore.Mvc;
using Via360.Shared.Models;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    // POST: api/usuarios/registrar-ciudadano
    [HttpPost("registrar-ciudadano")]
    public IActionResult RegistrarCiudadano([FromBody] Ciudadano nuevoCiudadano)
    {
        // El IdUsuario que llega aquí DEBE ser el UID que Firebase le dio a la App
        if (string.IsNullOrEmpty(nuevoCiudadano.IdUsuario))
            return BadRequest("Se requiere el UID de Firebase.");

        // Aquí es donde luego llamaremos a Firestore para guardar
        Console.WriteLine($"Registrando Ciudadano: {nuevoCiudadano.Nombre} con ID: {nuevoCiudadano.IdUsuario}");

        return Ok(new { Mensaje = "Perfil de ciudadano creado correctamente" });
    }

    // POST: api/usuarios/registrar-autoridad
    [HttpPost("registrar-autoridad")]
    public IActionResult RegistrarAutoridad([FromBody] Autoridad nuevaAutoridad)
    {
        // Solo un admin debería poder crear autoridades
        Console.WriteLine($"Registrando Autoridad: {nuevaAutoridad.Nombre} de la entidad: {nuevaAutoridad.Entidad}");

        return Ok(new { Mensaje = "Perfil de autoridad creado correctamente" });
    }
}