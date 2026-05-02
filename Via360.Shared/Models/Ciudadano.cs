using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public class Ciudadano : Usuario
    {
        public Ciudadano() : base() { }
        public Ciudadano(string id, string nombre, string email) : base (id, nombre, email) { }
    }
}
