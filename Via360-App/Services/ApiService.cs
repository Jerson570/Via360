using System.Buffers.Text;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Text.Json;
using Via360.Shared.Models;

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
        public async Task<string> ObtenerRolUsuario(string userId)
        {
            try
            {
                string endpoint = $"api/Usuarios/rol/{userId}";
                var response = await _httpClient.GetFromJsonAsync<RoleResponse>(endpoint);
                return response?.Rol ?? "Ciudadano";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($">>>>> ERROR GET ROL: {ex.Message}");
                return "Ciudadano";
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
        public async Task<List<Reporte>> ObtenerMisReportes(string userId)
        {
            try
            {
                string endpoint = $"api/Reportes/usuario/{userId}";
                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Reporte>>();
                }

                return new List<Reporte>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($">>>>> ERROR GET REPORTES: {ex.Message}");
                return null;
            }
        }
        public async Task<List<Reporte>> ObtenerReportesCercanosAsync()
        {
            try
            {
                string url = "api/Reportes/anonimos";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // 1. Deserializamos a un objeto dinámico/temporal para no romper tus clases
                    var listaPlana = JsonSerializer.Deserialize<List<JsonElement>>(json);
                    var reportesEstructurados = new List<Reporte>();

                    foreach (var elemento in listaPlana)
                    {
                        // 2. Mapeo manual: Construimos el objeto Reporte como TÚ lo necesitas
                        var reporte = new Reporte
                        {
                            IdReporte = elemento.GetProperty("id").GetString(),
                            Descripcion = elemento.GetProperty("descripcion").GetString(),
                            // Convertimos el string del JSON al Enum de C#
                            Tipo = Enum.Parse<TipoIncidente>(elemento.GetProperty("tipo").GetString()),
                            Estado = Enum.Parse<EstadoReporte>(elemento.GetProperty("estado").GetString()),

                            // AQUÍ ESTÁ LA MAGIA: Creamos el objeto Ubicacion que falta en el JSON
                            Ubicacion = new Ubicacion
                            {
                                Latitud = elemento.GetProperty("latitud").GetDouble(),
                                Longitud = elemento.GetProperty("longitud").GetDouble()
                            }
                        };
                        reportesEstructurados.Add(reporte);
                    }
                    return reportesEstructurados;
                }
                return new List<Reporte>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($">>>>> ERROR DESERIALIZACIÓN: {ex.Message}");
                return new List<Reporte>();
            }
        }
    }
    public class RoleResponse { public string Rol { get; set; } }
}