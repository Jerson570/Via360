using System.Buffers.Text;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Via360.App.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        // Reemplaza con tu URL real de Azure
        private readonly ConfiguracionService _config;

        public ApiService(ConfiguracionService config)
        {
            _config = config;

            if (string.IsNullOrEmpty(_config.UrlBackend))
            {
                throw new Exception("La URL del Backend no fue cargada desde el JSON");
            }

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_config.UrlBackend)
            };
        }

        public async Task<bool> RegistrarCiudadanoEnBackend(object usuarioData)
        {
            try
            {
                //endpoint concatenado automáticamente a la BaseAddress
                var response = await _httpClient.PostAsJsonAsync("api/Usuarios/registrar", usuarioData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error de conexión con Azure: {ex.Message}");
                return false;
            }
        }
    }
}