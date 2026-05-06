using Via360.App.ViewModels;
using Via360.App.Services;

namespace Via360.App.Pages;

public partial class PantallaPrincipal : ContentPage
{
    private Button? _botonActivo;
    private readonly MapaViewModel _viewModel;

    public PantallaPrincipal(MapaViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Carga inicial basada en la ubicación por defecto (Itagüí)
        // En el futuro, aquí usaremos Geolocation.GetLocationAsync()
        await _viewModel.CargarReportesAsync(6.17, -75.61);
    }

    // --- LÓGICA DE FILTROS (Interfaz) ---
    private void AplicarEstiloBoton(object sender)
    {
        if (_botonActivo != null)
        {
            _botonActivo.BackgroundColor = Color.FromArgb("#F0F0F0");
            _botonActivo.TextColor = Color.FromArgb("#512BD4");
        }

        if (sender is Button btn)
        {
            btn.BackgroundColor = Color.FromArgb("#512BD4");
            btn.TextColor = Colors.White;
            _botonActivo = btn;
        }
    }

    private void OnFiltrarTodos(object sender, EventArgs e)
    {
        AplicarEstiloBoton(sender);
        _viewModel.AplicarFiltroLocal("todos");
    }

    private void OnFiltrarBaches(object sender, EventArgs e)
    {
        AplicarEstiloBoton(sender);
        _viewModel.AplicarFiltroLocal("bache");
    }

    private void OnFiltrarSemaforos(object sender, EventArgs e)
    {
        AplicarEstiloBoton(sender);
        _viewModel.AplicarFiltroLocal("semaforo");
    }

    private void OnFiltrarAccidentes(object sender, EventArgs e)
    {
        AplicarEstiloBoton(sender);
        _viewModel.AplicarFiltroLocal("accidente");
    }

    // --- NAVEGACIÓN ---
    private async void OnNuevoReporteClicked(object sender, EventArgs e)
    {
        var paginaReporte = Handler.MauiContext.Services.GetService<CrearReportePage>();
        if (paginaReporte != null) await Navigation.PushModalAsync(paginaReporte);
    }

    private async void OnMisReportesClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("MisReportesPage");

    private async void OnPerfilClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("PerfilPage");
}