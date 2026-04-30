using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public class Reporte
    {
        public string Id { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Pendiente";
    }
}
