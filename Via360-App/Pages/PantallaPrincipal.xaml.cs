using Via360.Shared.Models;
using Via360.App.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace Via360.App.Pages;
public partial class PantallaPrincipal : ContentPage
{
    private readonly MapaService _mapaService = new MapaService();

  
    private string _filtroActual = "todos";
    private Button? _botonActivo;

    public ObservableCollection<Via360.Shared.Models.IncidenteReporte> Reportes { get; set; } = new();

    public int TotalPendientes => Reportes.Count(r => r.Estado == "pendiente");
    public int TotalEnProceso => Reportes.Count(r => r.Estado == "en_proceso");
    public int TotalResueltos => Reportes.Count(r => r.Estado == "resuelto");
    public int CantidadNotificaciones => 1; 
    public bool TieneNotificacionesNuevas => CantidadNotificaciones > 0;

    public string HtmlMapa
    {
        get
        {

            List<Via360.Shared.Models.IncidenteReporte> listaFiltrada = _filtroActual == "todos"
                ? Reportes.ToList()
                : Reportes.Where(r => r.Tipo == _filtroActual).ToList();

            return _mapaService.GenerarHtml(listaFiltrada);
        }
    }

    public PantallaPrincipal()
    {
        InitializeComponent();
        BindingContext = this;
        CargarReportesDePrueba();
    }

    private void CargarReportesDePrueba()
    {
        Reportes.Clear();
        Reportes.Add(new IncidenteReporte { Id = "1", Tipo = "bache", Descripcion = "Hueco grande en Cra 76", Latitud = 6.2318, Longitud = -75.6152, Estado = "pendiente", Votos = 14, FechaCreacion = DateTime.Now.AddDays(-1) });
        Reportes.Add(new IncidenteReporte { Id = "2", Tipo = "semaforo", Descripcion = "Semáforo apagado Cll 30", Latitud = 6.2305, Longitud = -75.6140, Estado = "en_proceso", Votos = 6, FechaCreacion = DateTime.Now.AddHours(-3) });
        Reportes.Add(new IncidenteReporte { Id = "3", Tipo = "accidente", Descripcion = "Choque leve en la esquina", Latitud = 6.2330, Longitud = -75.6165, Estado = "resuelto", Votos = 2, FechaCreacion = DateTime.Now.AddHours(-6) });

        ActualizarContadores();
    }

    private void ActualizarContadores()
    {
        OnPropertyChanged(nameof(TotalPendientes));
        OnPropertyChanged(nameof(TotalEnProceso));
        OnPropertyChanged(nameof(TotalResueltos));
        OnPropertyChanged(nameof(HtmlMapa));
    }

    // filtrar
    private void AplicarFiltro(object sender, string tipo)
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

        // Cambios en el mapa
        _filtroActual = tipo;
        OnPropertyChanged(nameof(HtmlMapa));
    }

    private async void OnNuevoReporteClicked(object sender, EventArgs e)
    {
        // En lugar de hacer 'new', le pide al Handler de la App 
        // que busque la página con todas sus dependencias inyectadas.
        var paginaReporte = Handler.MauiContext.Services.GetService<CrearReportePage>();

        if (paginaReporte != null)
        {
            await Navigation.PushModalAsync(paginaReporte);
        }
    }

    private async void OnMisReportesClicked(object sender, EventArgs e)
    {
        try
        {
            // Usamos la ruta directa que registramos en el paso anterior
            await Shell.Current.GoToAsync("MisReportesPage");
        }
        catch (Exception ex)
        {
            // Si falla, esto te dirá exactamente por qué en la consola de salida
            Console.WriteLine($"Error de navegación: {ex.Message}");
        }
    }

    private async void OnPerfilClicked(object sender, EventArgs e)
    {
        try
        {
            // Usamos el nombre que registramos en el AppShell
            await Shell.Current.GoToAsync("PerfilPage");
        }
        catch (Exception ex)
        {
            // Esto imprimirá el error real en la consola de Visual Studio
            System.Diagnostics.Debug.WriteLine($"Fallo en navegación: {ex.Message}");
            await this.DisplayAlertAsync("Error", "No se pudo abrir el perfil", "OK");
        }
    }

    private void OnFiltrarTodos(object sender, EventArgs e) => AplicarFiltro(sender, "todos");
    private void OnFiltrarBaches(object sender, EventArgs e) => AplicarFiltro(sender, "bache");
    private void OnFiltrarSemaforos(object sender, EventArgs e) => AplicarFiltro(sender, "semaforo");
    private void OnFiltrarAccidentes(object sender, EventArgs e) => AplicarFiltro(sender, "accidente");

    

    // cambios de barra de menu
    private async void OnExplorarClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("//PantallaPrincipal");
}