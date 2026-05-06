namespace Via360.App.Pages.Autoridad;

public partial class AutoridadPage : ContentPage
{
	public AutoridadPage()
	{
		InitializeComponent();
        CargarReportes();
	}
    private void CargarReportes()
    {
        // Esto es lo que tu amigo llenará con la API
        var listaEjemplo = new List<object>
        {
            new { Titulo = "Bache Crítico Calle 10", Fecha = DateTime.Now, Estado = "Pendiente" },
            new { Titulo = "Semáforo Averiado", Fecha = DateTime.Now.AddDays(-1), Estado = "En Revisión" },
            new { Titulo = "Grieta en Puente", Fecha = DateTime.Now.AddDays(-2), Estado = "Atendido" }
        };

        CrvReportesAutoridad.ItemsSource = listaEjemplo;
    }

    private async void OnReporteSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() != null)
        {
            // Deseleccionamos para que no se quede marcado el gris feo
            ((CollectionView)sender).SelectedItem = null;

            // Abrimos la página de detalle (la que quieres tipo NotificaciónPage)
            // Tu amigo solo tiene que crear esta página y pasarle el ID del reporte
            await Shell.Current.GoToAsync("DetalleReporteAutoridadPage");
        }
    }
}