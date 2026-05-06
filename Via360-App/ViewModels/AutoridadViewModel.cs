using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Via360.App.Services;
using Via360.Shared.Models;

public partial class AutoridadViewModel : ObservableObject
{
    private readonly ApiService _apiService;
    private readonly MapaService _mapaService;

    [ObservableProperty]
    private string htmlMapa;

    [ObservableProperty]
    private ObservableCollection<Reporte> reportes;

    public AutoridadViewModel(ApiService apiService, MapaService mapaService)
    {
        _apiService = apiService;
        _mapaService = mapaService;

        // Ejecución de carga inicial
        _ = CargarDatosAsync();
    }

    private async Task CargarDatosAsync()
    {
        // Usamos el ApiService que ya configuramos con el mapeo manual
        var lista = await _apiService.ObtenerReportesCercanosAsync();

        Reportes = new ObservableCollection<Reporte>(lista);

        // LLAMADA CLAVE: Pasamos 'true' para activar los colores de autoridad
        HtmlMapa = _mapaService.GenerarHtml(lista, true);
    }

    [RelayCommand]
    private async Task SeleccionarReporte(Reporte reporte)
    {
        if (reporte == null) return;
        await Shell.Current.GoToAsync($"DetalleReporteAutoridadPage?id={reporte.IdReporte}");
    }

    [RelayCommand]
    public async Task Logout()
    {
        bool confirmar = await Shell.Current.DisplayAlert("Cerrar Sesión", "¿Salir?", "Sí", "No");
        if (confirmar) await Shell.Current.GoToAsync("//PantallaPrincipal");
    }
}