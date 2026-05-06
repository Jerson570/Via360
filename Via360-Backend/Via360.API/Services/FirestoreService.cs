using FirebaseAdmin;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Via360.Shared.Models;

namespace Via360.Api.Services
{
    public class FirestoreService
    {
        private readonly FirestoreDb _db;
        public FirestoreService(IConfiguration configuration)
        {
            var projectId = configuration["FirebaseSettings:ProjectId"];
            var jsonContent = configuration["FIREBASE_JSON_CONTENT"]; // variable azure

            if (string.IsNullOrEmpty(projectId))
            {
                throw new Exception("ERROR CRÍTICO: El ProjectId de Firebase no está configurado en appsettings");
            }
            FirestoreClient client;

            if (!string.IsNullOrEmpty(jsonContent))
            {
                // si estamos en azure
                client = new FirestoreClientBuilder
                {
                    JsonCredentials = jsonContent
                }.Build();
            }
            else
            {
                // si estamos en local
                var keyPath = configuration["FirebaseSettings:CredentialFilePath"];

                if (string.IsNullOrEmpty(keyPath) || !File.Exists(keyPath))
                {
                    throw new Exception("ERROR CRÍTICO: La llave de Firebase no está configurada o no se encontró en appsettings");
                }

                client = new FirestoreClientBuilder
                {
                    CredentialsPath = keyPath
                }.Build();
            }
            _db = FirestoreDb.Create(projectId, client);
        }
        public async Task GuardarUsuario(Usuario usuario)
        {
            // Usamos el IdUsuario (UID de Firebase) como el nombre del documento
            DocumentReference docRef = _db.Collection("usuarios").Document(usuario.IdUsuario);

            // Convertimos el objeto a un diccionario para Firestore
            await docRef.SetAsync(new
            {
                nombre = usuario.Nombre,
                email = usuario.Email,
                rol = "Ciudadano", //Hardcodeado por seguridad
                fechaRegistro = Timestamp.FromDateTime(usuario.FechaRegistro.ToUniversalTime()),
                cargo = usuario.Cargo, // Será null si es ciudadano
                entidad = usuario.Entidad // Será null si es ciudadano
            });
        }
        public async Task GuardarReporte(Reporte reporte)
        {
            // se genera aquí si viene vacío para asegurar unicidad
            var idDocumento = string.IsNullOrEmpty(reporte.IdReporte) 
                            ? Guid.NewGuid().ToString() 
                            : reporte.IdReporte;
            DocumentReference docRef = _db.Collection("reportes").Document(idDocumento);

            await docRef.SetAsync(new
            {
                idReporte = idDocumento,
                idUsuario = reporte.IdUsuario,
                fecha = Timestamp.FromDateTime(reporte.Fecha.ToUniversalTime()),
                tipo = reporte.Tipo.ToString(), //guardado como string para que sea legible
                descripcion = reporte.Descripcion,
                estado = reporte.Estado.ToString(),
                ubicacion = new
                {
                    latitud = reporte.Ubicacion.Latitud,
                    longitud = reporte.Ubicacion.Longitud,
                    direccionTexto = reporte.Ubicacion.DireccionTexto
                }
            });
        }
        public async Task<List<Reporte>> ObtenerReportesPorUsuario(string uid)
        {
            CollectionReference reportesRef = _db.Collection("reportes");
            Query consulta = reportesRef.WhereEqualTo("idUsuario", uid).OrderByDescending("fecha");
            QuerySnapshot snapshot = await consulta.GetSnapshotAsync();

            var listaReportes = new List<Reporte>();

            foreach (DocumentSnapshot item in snapshot.Documents)
            {
                if (!item.Exists) continue;

                var data = item.ToDictionary();

                // se extrae la ubicación (que es un mapa/diccionario en Firestore)
                var ubicacionData = data["ubicacion"] as Dictionary<string, object>;

                var reporte = new Reporte
                {
                    IdReporte = data["idReporte"]?.ToString(),
                    IdUsuario = data["idUsuario"]?.ToString(),
                    Descripcion = data["descripcion"]?.ToString(),
                    // se convierte el string de la DB de vuelta al Enum de C#
                    Tipo = Enum.Parse<TipoIncidente>(data["tipo"]?.ToString() ?? "Otro"),
                    Estado = Enum.Parse<EstadoReporte>(data["estado"]?.ToString() ?? "Pendiente"),
                    // Manejo de la fecha de Firestore
                    Fecha = ((Timestamp)data["fecha"]).ToDateTime(),
                    Ubicacion = new Ubicacion(
                        Convert.ToDouble(ubicacionData["latitud"]),
                        Convert.ToDouble(ubicacionData["longitud"]),
                        ubicacionData["direccionTexto"]?.ToString()
                    )
                };

                listaReportes.Add(reporte);
            }

            return listaReportes;
        }
        public async Task<List<object>> ObtenerReportesAnonimos()
        {
            try
            {
                // 1. Apuntamos a la colección de reportes
                CollectionReference reportesRef = _db.Collection("reportes");

                // 2. Traemos los últimos 100 para no reventar la cuota de lectura
                QuerySnapshot snapshot = await reportesRef.OrderByDescending("fecha").Limit(100).GetSnapshotAsync();

                var listaAnonima = new List<object>();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (doc.Exists)
                    {
                        var data = doc.ToDictionary();

                        double lat = 0;
                        double lon = 0;

                        if (data.ContainsKey("ubicacion") && data["ubicacion"] is IDictionary<string, object> ub)
                        {
                            lat = ub.ContainsKey("latitud") ? Convert.ToDouble(ub["latitud"]) : 0;
                            lon = ub.ContainsKey("longitud") ? Convert.ToDouble(ub["longitud"]) : 0;
                        }
                        // Mapeamos solo lo necesario para el mapa 
                        listaAnonima.Add(new
                        {
                            id = doc.Id,
                            tipo = data.ContainsKey("tipo") ? data["tipo"].ToString() : "Incidente",
                            descripcion = data.ContainsKey("descripcion") ? data["descripcion"].ToString() : "Sin descripción", // Agregado
                            latitud = lat,
                            longitud = lon,
                            estado = data.ContainsKey("estado") ? data["estado"].ToString() : "Pendiente"
                        });
                    }
                }
                return listaAnonima;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR CRÍTICO EN FIREBASE: {ex.Message}");
                return new List<object>();
            }
        }
        public async Task<string> ObtenerRolUsuarioAsync(string uid)
        {
            try
            {
                DocumentReference docRef = _db.Collection("usuarios").Document(uid);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    return snapshot.GetValue<string>("rol");
                }
                return "Ciudadano"; // Por seguridad, si no se encuentra el usuario, se asume rol Ciudadano
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR CRÍTICO EN FIREBASE: {ex.Message}");
                return "Ciudadano"; // En caso de error, por seguridad, se asume rol Ciudadano
            }
        }
    }
}