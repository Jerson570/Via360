using System;
using System.Collections.Generic;
using System.Text;
namespace Via360.Shared.Models
{
    public abstract class Usuario
    {
        public string IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro {  get; set; }
        public Usuario() { }
        public Usuario(string id, string nombre, string email)
        {
            this.IdUsuario = id;
            this.Nombre = nombre;
            this.Email = email;
            this.FechaRegistro = DateTime.Now;
        }
    }
}
