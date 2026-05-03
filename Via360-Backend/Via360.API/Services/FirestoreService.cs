using FirebaseAdmin;
using Google.Cloud.Firestore;
using Via360.Shared.Models;

namespace Via360.Api.Services
{
    public class FirestoreService
    {
        private readonly FirestoreDb _db;
        public FirestoreService(IConfiguration configuration)
        {
            var projectId = configuration["FirebaseSettings:ProjectId"];
            if (string.IsNullOrEmpty(projectId))
            {
                throw new Exception("ERROR CRÍTICO: El ProjectId de Firebase no está configurado en appsettings");
            }
            // El ProjectId debe coincidir con "via360-app" que configuramos antes
            _db = FirestoreDb.Create("via360-app");
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
    }
}