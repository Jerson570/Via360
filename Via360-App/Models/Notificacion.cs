using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.App.Models
{
    public class Notificacion
    {
        public string Id { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string DescripcionDetallada { get; set; } = string.Empty;
        public string FotoUrl { get; set; } = "https://via.placeholder.com/400x200.png?text=Sin+Imagen";
        public string Resumen => DescripcionDetallada.Length > 50 ? DescripcionDetallada.Substring(0, 50) + "..." : DescripcionDetallada;
        public DateTime Fecha { get; set; }
        public bool FueLeida { get; set; } 
        public string Icono { get; set; } = "🔔";
    }
}
