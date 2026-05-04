using System;
using System.Collections.Generic;
using System.Text;
namespace Via360.Shared.Models
{
    public class Usuario
    {
        public string IdUsuario { get; set; } // UID de firebase
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; } // "Ciudadano" o "Autoridad"
        public DateTime FechaRegistro {  get; set; }

        // campos exclusivos de autoridades (NULL para ciudadanos)
        public string? Cargo { get; set; }
        public string? Entidad { get; set; }

        public Usuario() { }
        public Usuario(string id, string nombre, string email, string rol = "Ciudadano")
        {
            this.IdUsuario = id;
            this.Nombre = nombre;
            this.Email = email;
            this.Rol = rol;
        }
    }
}
