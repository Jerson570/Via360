using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.Shared.Models
{
    public class Reporte
    {
        private string idReporte { get; set; }
        private DateTime fecha { get; set; }
        private TipoIncidente tipo { get; set; }
        private string descripcion { get; set; }
        private Estado estado {  get; set; }
        public Reporte()
        {
            // Constructor vacío
        }
        public Reporte(TipoIncidente tipoInicial, string descripcion, Estado estadoInicial)
        {
            this.tipo = tipoInicial;
            this.descripcion=descripcion;
            this.fecha= DateTime.Now;
            this.estado=estadoInicial;
        }
    }
}
