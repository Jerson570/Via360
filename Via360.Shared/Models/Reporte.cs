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

        public Reporte() { }

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
        public string TipoTextoAmigable => ObtenerTextoAmigable(Tipo);

        private string ObtenerTextoAmigable(Enum valor)
        {
            var fi = valor.GetType().GetField(valor.ToString());
            var attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : valor.ToString();
        }
        public string EstadoTexto => Estado switch
        {
            EstadoReporte.Pendiente => "Pendiente",
            EstadoReporte.EnProceso => "En Proceso", // Aquí le pones el espacio
            EstadoReporte.Resuelto => "Resuelto",
            _ => Estado.ToString()
        };

        // 2. Color dinámico para el badge (OPCIONAL pero recomendado)
        public string EstadoColorHex => Estado switch
        {
            EstadoReporte.Pendiente => "#FF3B30", // Rojo
            EstadoReporte.EnProceso => "#FF9500", // Naranja
            EstadoReporte.Resuelto => "#4CD964",  // Verde
            _ => "#512BD4"                        // Morado
        };
    }

}
