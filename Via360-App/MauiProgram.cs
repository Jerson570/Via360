using Microsoft.Extensions.Logging;
using Via360.App.Services;

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
            return builder.Build();
        }
    }
}
