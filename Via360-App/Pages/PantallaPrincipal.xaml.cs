using login.Models;
using login.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace login.Pages;
public partial class PantallaPrincipal : ContentPage
{
    private readonly MapaService _mapaService = new MapaService();

    // 1. Necesitamos esta variable para saber qué filtro aplicar
    private string _filtroActual = "todos";
    private Button? _botonActivo;

    public ObservableCollection<IncidenteReporte> Reportes { get; set; } = new();

    public int TotalPendientes => Reportes.Count(r => r.Estado == "pendiente");
    public int TotalEnProceso => Reportes.Count(r => r.Estado == "en_proceso");
    public int TotalResueltos => Reportes.Count(r => r.Estado == "resuelto");

    // 2. Modificamos el "get" para que filtre de verdad antes de generar el HTML
    public string HtmlMapa
    {
        get
        {
            var listaFiltrada = _filtroActual == "todos"
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

    // 3. LÓGICA DE FILTRADO MEJORADA
    private void AplicarFiltro(object sender, string tipo)
    {
        // Feedback visual del botón
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

        // Cambiamos el valor del filtro y notificamos el cambio al mapa
        _filtroActual = tipo;
        OnPropertyChanged(nameof(HtmlMapa));
    }

    private void OnFiltrarTodos(object sender, EventArgs e) => AplicarFiltro(sender, "todos");
    private void OnFiltrarBaches(object sender, EventArgs e) => AplicarFiltro(sender, "bache");
    private void OnFiltrarSemaforos(object sender, EventArgs e) => AplicarFiltro(sender, "semaforo");
    private void OnFiltrarAccidentes(object sender, EventArgs e) => AplicarFiltro(sender, "accidente");

    private async void OnNuevoReporteClicked(object sender, EventArgs e)
    {
        await this.DisplayAlertAsync("Vía360", "Próximamente: Crear reporte", "OK");
    }

    // --- EVENTOS DE NAVEGACIÓN (Para que no den error) ---
    private async void OnExplorarClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("//PantallaPrincipal");
    private async void OnNotificacionesClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("//NotificacionesPage");
    private async void OnMisReportesClicked(object sender, EventArgs e) => await this.DisplayAlertAsync("Mis Vías", "Aún no has reportado nada.", "OK");
    private async void OnPerfilClicked(object sender, EventArgs e) => await this.DisplayAlertAsync("Perfil", "Ajustes de cuenta.", "OK");
}