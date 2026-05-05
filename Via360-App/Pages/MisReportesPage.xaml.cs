using System.Collections.ObjectModel;
using Via360.Shared.Models;

namespace Via360.App.Pages;

public partial class MisReportesPage : ContentPage
{
	public MisReportesPage()
	{
		InitializeComponent();
	}
    public ObservableCollection<IncidenteReporte> MisReportes { get; set; } = new ObservableCollection<IncidenteReporte>();
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarReportesDelUsuario();
    }

    private void CargarReportesDelUsuario()
    {
        // Limpiamos la lista para evitar duplicados al entrar y salir de la página
        MisReportes.Clear();

        // Aquí es donde haremos la consulta a Firebase filtrando por el ID del usuario logueado.

    }
}