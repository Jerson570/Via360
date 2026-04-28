using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.App.Models
{
    public class IncidenteReporte
    {
        public string? Id { get; set; }
        public string ?Tipo { get; set; }
        public string? Descripcion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Votos { get; set; }

        public string IconoTipo => Tipo switch
        {
            "bache" => "🕳️",
            "semaforo" => "🚦",
            "accidente" => "🚨",
            _ => "📍"
        };

        public string IconoEstado => Estado switch
        {
            "pendiente" => "🔴",
            "en_proceso" => "🟡",
            "resuelto" => "🟢",
            _ => "⚪"
        };

        public Color ColorEstado => Estado switch
        {
            "pendiente" => Color.FromArgb("#FF4757"),
            "en_proceso" => Color.FromArgb("#FFA502"),
            "resuelto" => Color.FromArgb("#2ED573"),
            _ => Color.FromArgb("#A4B0BE")
        };
    }
}
