using System;
using System.Collections.Generic;
using System.Text;
using Google.Cloud.Firestore;

namespace Via360.Shared.Models
{
    public class Ubicacion
    {
        [FirestoreProperty("latitud")]
        public double Latitud { get; set; }
        [FirestoreProperty("longitud")]
        public double Longitud { get; set; }
        [FirestoreProperty("direccionTexto")]
        public string? DireccionTexto { get; set; }
        public Ubicacion() { }
        public Ubicacion(double latitud, double longitud, string direccion = "")
        {
            this.Latitud = latitud;
            this.Longitud = longitud;
            this.DireccionTexto = direccion;
        }
    }
}
