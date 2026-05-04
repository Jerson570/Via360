using Microsoft.Extensions.DependencyInjection;
using Via360.App.Services;

namespace Via360.App
{
    public partial class App : Application
    {
        public App(ConfiguracionService config, IAuthService authService)
        {
            InitializeComponent();

            // PRUEBA TÉCNICA
            var nombreNube = config.CloudName;
            var preset = config.UploadPreset;

            System.Diagnostics.Debug.WriteLine($"[PRUEBA] Cloud: {nombreNube}, Preset: {preset}");
            MainPage = new AppShell();

            // PRUEBA RÁPIDA: No borrar esto hasta que funcione
            Task.Run(async () => 
            {
                var resultado = await authService.RegistroAsync("test_via360@pascualbravo.edu.co", "Ingenieria123!");
                if (resultado != null)
                    System.Diagnostics.Debug.WriteLine($">>>>> ÉXITO: Usuario creado con UID: {resultado}");
                else
                    System.Diagnostics.Debug.WriteLine(">>>>> FALLO: El túnel sigue cerrado. Revisa el SHA-1 o la API Key.");
            });
        }
    }
}