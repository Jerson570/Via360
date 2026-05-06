using Via360.App.Pages;

namespace Via360.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PantallaPrincipal), typeof(PantallaPrincipal));
            Routing.RegisterRoute(nameof(Via360.App.Pages.Autoridad.AutoridadPage), typeof(Via360.App.Pages.Autoridad.AutoridadPage));
            Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
            Routing.RegisterRoute(nameof(RecuperarPage), typeof(RecuperarPage));
            Routing.RegisterRoute(nameof(Via360.App.Pages.MisReportesPage), typeof(Via360.App.Pages.MisReportesPage));
            Routing.RegisterRoute(nameof(Via360.App.Pages.PerfilPage), typeof(Via360.App.Pages.PerfilPage));
        }
    }
}
