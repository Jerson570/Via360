using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public class Ubicacion
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string DireccionTexto { get; set; }
        public Ubicacion() { }
        public Ubicacion(double latitud, double longitud, string direccion = "")
        {
            this.Latitud = latitud;
            this.Longitud = longitud;
            this.DireccionTexto = direccion;
        }
    }
}
