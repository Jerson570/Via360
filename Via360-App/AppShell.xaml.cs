using Via360.App.Pages;

namespace Via360.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PantallaPrincipal), typeof(PantallaPrincipal));
            Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
            Routing.RegisterRoute(nameof(RecuperarPage), typeof(RecuperarPage));
            Routing.RegisterRoute(nameof(PerfilPage), typeof(PerfilPage));
        }
    }
}
