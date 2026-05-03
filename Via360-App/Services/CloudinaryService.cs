namespace Via360.App.Services
{
    public class CloudinaryService : IImagenService
    {
        private readonly ConfiguracionService _config;
        private readonly HttpClient _httpClient;

        public CloudinaryService(ConfiguracionService config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        public async Task<string> SubirImagenAsync(FileResult foto)
        {
            if (foto == null) return null;

            // Usamos las propiedades que el servicio leyó del JSON
            var url = $"https://api.cloudinary.com/v1_1/{_config.CloudName}/image/upload";

            using var content = new MultipartFormDataContent();

            // Abre el stream de la foto tomada con el celular
            var stream = await foto.OpenReadAsync();
            content.Add(new StreamContent(stream), "file", foto.FileName);

            // El preset escondido en el appsettings.json
            content.Add(new StringContent(_config.UploadPreset), "upload_preset");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                // Aquí se debería usar una librería como Newtonsoft.Json o System.Text.Json 
                // para extraer la "secure_url" del JSON de respuesta de Cloudinary
                return responseString;
            }

            return null;
        }
    }
}