using Microsoft.Extensions.Logging;
using Via360.App.Pages;
using Via360.App.Services;
using Via360.App.ViewModels;

namespace Via360.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //lee el json
            builder.Services.AddSingleton<ConfiguracionService>();
            //sube las fotos (interfaz+implementacion)
            builder.Services.AddSingleton<IImagenService, CloudinaryService>();
            //autenticación
            builder.Services.AddSingleton<IAuthService, FirebaseAuthService>();

            // registro de ViewModels
            builder.Services.AddTransient<LoginViewModel>();

            // registro de Páginas
            builder.Services.AddTransient<PantallaPrincipal>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<PerfilPage>();
            builder.Services.AddTransient<RegistroPage>();
            builder.Services.AddTransient<RecuperarPage>();
            return builder.Build();
        }
    }
}
