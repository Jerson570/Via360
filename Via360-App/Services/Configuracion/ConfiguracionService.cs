using System.Reflection;
using System.Text.Json;

namespace Via360.App.Services
{
    public class ConfiguracionService
    {
        public string CloudName { get; private set; }
        public string UploadPreset { get; private set; }
        public string FirebaseApiKey { get; private set; }
        public string UrlBackend { get; private set; }

        public ConfiguracionService()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ConfiguracionService)).Assembly;

                Stream stream = assembly.GetManifestResourceStream("appsettings.json");

                if (stream == null)
                {
                    // para ver en la consola qué nombres de recursos existen realmente
                    var nombres = assembly.GetManifestResourceNames();
                    throw new Exception($"ERROR: Solo existen estos recursos: {string.Join(", ", nombres)}");
                }

                using var reader = new StreamReader(stream);

                var json = reader.ReadToEnd();
                var config = JsonDocument.Parse(json);

                //lectura de cloudinary
                CloudName = config.RootElement.GetProperty("Cloudinary").GetProperty("CloudName").GetString();
                UploadPreset = config.RootElement.GetProperty("Cloudinary").GetProperty("UploadPreset").GetString();

                //lectura de Firebase
                FirebaseApiKey = config.RootElement.GetProperty("Firebase").GetProperty("ApiKey").GetString();

                // lectura de backend Azure
                UrlBackend = config.RootElement.GetProperty("Azure").GetProperty("UrlBackend").GetString();
            }
            catch (Exception ex)
            {
                 
                throw new Exception($"ERROR CRÍTICO EN CONFIGURACIÓN: {ex.Message}", ex);
            }
        }
    }
}