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

    private async Task ActualizarPosicionGPS()
    {
        try
        {
            //Permiso de ubicación
            PermissionStatus status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted) return;

            // Obtener ubicacion
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));

            if (location != null)
            {
                // 3. Formatear el comando JS (usando cultura invariante para evitar problemas con comas/puntos)
                string lat = location.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string lon = location.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

                // Llamamos a la función que escribimos en el MapaService
                await MiWebView.EvaluateJavaScriptAsync($"actualizarUbicacionUsuario({lat}, {lon})");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error al obtener ubicación: {ex.Message}");
        }

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Obtenemos su ubicación para cargar reportes cercanos (Itagüí por ahora)
        // Esto conectará con tu lógica de 'CargarReportesAsync' en el ViewModel
        await _viewModel.CargarReportesAsync(6.17, -75.61);
        // se ubica al usuario
        _ = IniciarSeguimientoUbicacion();

    }
    private async Task IniciarSeguimientoUbicacion()
    {
        while (this.IsLoaded)
        {
            try
            {
                // 1. Pedir permisos si no los hay
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status == PermissionStatus.Granted)
                {
                    // 2. Obtener ubicación (Ajusta la precisión para ahorrar batería)
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(5));
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        // 3. Inyectar al WebView
                        string js = $"actualizarUbicacionUsuario({location.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {location.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
                        await MiWebView.EvaluateJavaScriptAsync(js);
                    }
                }
            }
            catch (Exception ex) { /* Manejar error de sensor */ }
            await Task.Delay(10000); // Esperar 10 segundos antes de la siguiente actualización
        }
        
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
    private async void OnCentrarUsuarioClicked(object sender, EventArgs e)
    {
        await MiWebView.EvaluateJavaScriptAsync("centrarEnUsuario()");
    }
    private async void OnWebViewNavigated(object sender, WebNavigatedEventArgs e)
    {
        // Solo cuando el WebView dice "Ya cargué todo", buscamos el GPS
        await ActualizarPosicionGPS();
    }

}