using System.Windows.Input;
using Via360.App.Pages;
using Via360.App.Pages.Autoridad;

namespace Via360.App
{
    public partial class AppShell : Shell
    {
        public ICommand LogoutCommand => new Command(async () => await AlCerrarSesion());   
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;

            Routing.RegisterRoute(nameof(PantallaPrincipal), typeof(PantallaPrincipal));
            Routing.RegisterRoute(nameof(Via360.App.Pages.Autoridad.AutoridadPage), typeof(Via360.App.Pages.Autoridad.AutoridadPage));
            Routing.RegisterRoute(nameof(DetalleReporteAutoridadPage), typeof(DetalleReporteAutoridadPage));
            Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
            Routing.RegisterRoute(nameof(RecuperarPage), typeof(RecuperarPage));
            Routing.RegisterRoute(nameof(Via360.App.Pages.MisReportesPage), typeof(Via360.App.Pages.MisReportesPage));
            Routing.RegisterRoute(nameof(Via360.App.Pages.PerfilPage), typeof(Via360.App.Pages.PerfilPage));
        }

        private async Task AlCerrarSesion()
        {
            bool respuesta = await Shell.Current.DisplayAlert("Cerrar Sesión", "¿Deseas salir?", "Sí", "No");

            if (respuesta)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    // El "//" resetea la pila, pero cerrar el Flyout antes asegura que no quede el "fantasma" abierto
                    await Shell.Current.GoToAsync("//MainPage");
                });
            }
        }
    }
}
