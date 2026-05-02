using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public class Reporte
    {
        public string IdReporte { get; private set; }
        public DateTime Fecha { get; private set; }
        public TipoIncidente Tipo { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public EstadoReporte Estado {  get; set; }
        public Reporte()
        {
            // Constructor vacío
        }
        public Reporte(TipoIncidente tipoInicial, Usuario usuario, string descripcion, string ubicacion, EstadoReporte estadoInicial)
        {
            this.IdReporte = Guid.NewGuid().ToString();
            this.Fecha = DateTime.Now;
            this.Tipo = tipoInicial;
            this.Ubicacion = ubicacion;
            this.Descripcion = descripcion;
            this.Estado = EstadoReporte.Pendiente;
        }
    }
}
