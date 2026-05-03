using Microsoft.Extensions.DependencyInjection;
using Via360.App.Services;

namespace Via360.App
{
    public partial class App : Application
    {
        public App(ConfiguracionService config)
        {
            InitializeComponent();

            // PRUEBA TÉCNICA
            var nombreNube = config.CloudName;
            var preset = config.UploadPreset;

            System.Diagnostics.Debug.WriteLine($"[PRUEBA] Cloud: {nombreNube}, Preset: {preset}");
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}