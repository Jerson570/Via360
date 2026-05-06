using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;

namespace Via360.Shared.Models
{
    public class Reporte
    {
        [FirestoreProperty("idReporte")]
        public string? IdReporte { get; set; }
        [FirestoreProperty("idUsuario")]
        public string IdUsuario { get; set; }
        [FirestoreProperty("fecha")]
        public DateTime Fecha { get; set; }
        [FirestoreProperty("tipo")]
        public TipoIncidente Tipo { get; set; }
        [FirestoreProperty("ubicacion")]
        public Ubicacion Ubicacion { get; set; }
        [FirestoreProperty("descripcion")]
        public string Descripcion { get; set; }
        [FirestoreProperty("estado")]
        public EstadoReporte Estado {  get; set; }
        [FirestoreProperty("urlImagen")]
        public string? UrlImagen { get; set; }
        public Reporte()
        {

        }
    
        public Reporte(string IdUsuario, TipoIncidente tipoInicial, string descripcion, Ubicacion ubicacion, EstadoReporte estadoInicial, string UrlImagen)
        {
            this.IdReporte = Guid.NewGuid().ToString();
            this.IdUsuario = IdUsuario;
            this.Fecha = DateTime.Now;
            this.Tipo = tipoInicial;
            this.Descripcion = descripcion;
            this.Ubicacion = ubicacion;
            this.Estado = EstadoReporte.Pendiente;
            this.UrlImagen = UrlImagen;
        }
    }
}
