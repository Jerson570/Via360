using System.Buffers.Text;
using System.Net.Http.Json;
using System.Diagnostics;
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
                string endpoint = "api/Usuarios/registrar";
                System.Diagnostics.Debug.WriteLine($">>>>>>>> LLAMANDO A: {_httpClient.BaseAddress}{endpoint}");
                var response = await _httpClient.PostAsJsonAsync(endpoint, usuarioData);

                if (!response.IsSuccessStatusCode)
                {
                    var contenidoError = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($" >>>>>>>>> AZURE RESPONDIÓ: ({response.StatusCode}): {contenidoError}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($">>>>> ERROR CRÍTICO DE RED: {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($">>>>> DETALLE: {ex.InnerException.Message}");
                }
                return false;
            }
        }
        public async Task<bool> GuardarReporte(object reporteData)
        {
            try
            {
                string endpoint = "api/Reportes/crear";
                Debug.WriteLine($">>>>>>>> LLAMANDO A: {_httpClient.BaseAddress}{endpoint}");
                var response = await _httpClient.PostAsJsonAsync(endpoint, reporteData);
                if (!response.IsSuccessStatusCode)
                {
                    var contenidoError = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($" >>>>>>>>> AZURE RESPONDIÓ: ({response.StatusCode}): {contenidoError}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($">>>>> ERROR CRÍTICO DE RED: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($">>>>> DETALLE: {ex.InnerException.Message}");
                }
                return false;
            }
        }
    }
}