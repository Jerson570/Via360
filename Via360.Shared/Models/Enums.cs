using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public enum TipoIncidente
    {
        Accidente,
        [Description("Semáforo Averiado")]
        SemáforoAveriado,
        Bache,
        [Description("Obstrucción Vial")]
        ObstrucciónVial,
        [Description("Obra en la Vía")]
        ObraEnLaVía,
        Otro
    }
    public enum EstadoReporte
    {
        Pendiente,
        EnProceso,
        Resuelto
    }
}
