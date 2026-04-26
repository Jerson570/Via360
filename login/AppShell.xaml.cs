namespace login
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
            Routing.RegisterRoute(nameof(RecuperarPage), typeof(RecuperarPage));
        }
    }
}
