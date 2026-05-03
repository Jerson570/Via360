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
            var keyPath = configuration["FirebaseSettings:CredentialFilePath"];
            if (string.IsNullOrEmpty(projectId))
            {
                throw new Exception("ERROR CRÍTICO: El ProjectId de Firebase no está configurado en appsettings");
            }
            if (string.IsNullOrEmpty(keyPath) || !File.Exists(keyPath))
            {
                throw new Exception("ERROR CRÍTICO: La llave de Firebase no está configurada o no se encontró en appsettings");
            }
            // constructor del cliente con credenciales explícitas
            FirestoreClient client = new FirestoreClientBuilder
            {
                CredentialsPath = keyPath
            }.Build();

            // El ProjectId debe coincidir con "via360-app"
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
                rol = usuario.Rol,
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
    }
}