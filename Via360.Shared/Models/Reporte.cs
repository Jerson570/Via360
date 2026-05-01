using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public class Reporte
    {
        private string idReporte { get; set; } = "string.Empty";
        private DateTime fecha { get; set; } = DateTime.Now;
        private enum TipoIncidente
        {
            Accidente,
            SemáforoAveriado,
            Bache,
            ObstrucciónVial,
            ObraEnLaVía,
            Otro
        }
        private string descripcion { get; set; } = "";
        private enum Estado
        {
            Pendiente,
            EnProceso,
            Resuelto
        }
    }
}
