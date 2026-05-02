using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public class Autoridad : Usuario
    {
        public string Entidad { get; set; }
        public string Cargo { get; set; }
        public Autoridad() : base() { }
        public Autoridad(string id, string nombre, string email, string entidad, string cargo) : base(id, nombre, email)
        {
            this.Entidad = entidad;
            this.Cargo = cargo;
        }
    }
}
