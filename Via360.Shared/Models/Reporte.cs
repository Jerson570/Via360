using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public class Reporte
    {
        public string IdReporte { get; set; }
        public string IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public TipoIncidente Tipo { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public EstadoReporte Estado {  get; set; }
        public Reporte()
        {

        }
        public Reporte(string IdUsuario, TipoIncidente tipoInicial, string descripcion, Ubicacion ubicacion, EstadoReporte estadoInicial)
        {
            this.IdReporte = Guid.NewGuid().ToString();
            this.IdUsuario = IdUsuario;
            this.Fecha = DateTime.Now;
            this.Tipo = tipoInicial;
            this.Descripcion = descripcion;
            this.Ubicacion = ubicacion;
            this.Estado = EstadoReporte.Pendiente;
        }
    }
}
