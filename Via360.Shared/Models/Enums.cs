using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public enum TipoIncidente
    {
        Accidente,
        SemáforoAveriado,
        Bache,
        ObstrucciónVial,
        ObraEnLaVía,
        Otro
    }
    public enum Estado
    {
        Pendiente,
        EnProceso,
        Resuelto
    }
}
