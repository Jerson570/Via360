using Microsoft.AspNetCore.Mvc;
using Via360.Api.Services;
using Via360.Shared.Models;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly FirestoreService _firestoreService;
    public UsuariosController(FirestoreService firestoreService)
    {
        _firestoreService = firestoreService;
    }

    // POST: api/Usuarios/registrar
    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] Usuario usuario)
    {
        if (usuario == null || string.IsNullOrEmpty(usuario.IdUsuario))
        {
            return BadRequest("Datos de usuario inválidos o UID faltante.");
        }
        try
        {
            await _firestoreService.GuardarUsuario(usuario);
            return Ok(new { mensaje = "Usuario guardado exitosamente en Firestore" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno: {ex.Message}");
        }
    }
}