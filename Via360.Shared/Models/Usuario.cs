using System;
using System.Collections.Generic;
using System.Text;
namespace Via360.Shared.Models
{
    public abstract class Usuario
    {
        public string IdUsuario { get; private set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string HashContraseña { get; set; }
        public Usuario() { }
        public Usuario(string nombre, string email, string ContraseñaPlana)
        {
            this.IdUsuario = Guid.NewGuid().ToString();
            this.Nombre = nombre;
            this.Email = email;
        }
    }
}
