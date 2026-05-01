using Via360.App.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Via360.App.Pages;

public partial class NotificacionesPage : ContentPage
{
    public ObservableCollection<Notificacion> MisNotificaciones { get; set; } = new();

    // Propiedad para el botón de leer todo
    public bool HayNotificacionesSinLeer => MisNotificaciones.Any(n => !n.FueLeida);

    private Notificacion _selectedNotif;
    public Notificacion SelectedNotif
    {
        get => _selectedNotif;
        set
        {
            _selectedNotif = value;
            OnPropertyChanged(nameof(SelectedNotif));
        }
    }

    public NotificacionesPage()
    {
        InitializeComponent();
        CargarNotificaciones();
        BindingContext = this;
    }

    private void CargarNotificaciones()
    {
        MisNotificaciones.Add(new Notificacion
        {
            Id = "101",
            Titulo = "¡Reparación Iniciada!",
            DescripcionDetallada = "La cuadrilla de bacheo ha llegado a la Cra 76. El tráfico estará restringido a un carril durante las próximas 4 horas.",
            Fecha = DateTime.Now,
            FueLeida = false,
            Icono = "🚧",
            FotoUrl = "https://noticias.medellin.gov.co/wp-content/uploads/2021/04/Bacheo-1.jpg"
        });
    }


    // cuadro de detalles al tocar una notificación
    private void OnNotificacionTapped(object sender, TappedEventArgs e)
    {
        var notif = e.Parameter as Notificacion;
        if (notif == null) return;

        // 1. Asignar los datos al Popup
        SelectedNotif = notif;

        // 2. Mostrar el Popup
        PopupDetalle.IsVisible = true;

        // 3. Marcar como leída y refrescar
        notif.FueLeida = true;
        RefrescarLista();
    }

    private void OnCerrarPopupClicked(object sender, EventArgs e)
    {
        // Oculta el Popup
        PopupDetalle.IsVisible = false;
    }

    private void OnMarcarTodoLeidoClicked(object sender, EventArgs e)
    {
        foreach (var notif in MisNotificaciones)
        {
            notif.FueLeida = true;
        }
        RefrescarLista();
    }

    private void RefrescarLista()
    {
        // redibujo de la lista para que se actualicen los cambios visuales (color, botón, etc)
        var temp = MisNotificaciones.ToList();
        MisNotificaciones.Clear();
        foreach (var item in temp) MisNotificaciones.Add(item);

        OnPropertyChanged(nameof(HayNotificacionesSinLeer));
    }

    // manejo del botón de retroceso para cerrar el popup en vez de salir de la página
    protected override bool OnBackButtonPressed()
    {
        
        if (PopupDetalle.IsVisible)
        {
            PopupDetalle.IsVisible = false;
            return true; 
        }

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//PantallaPrincipal");
        });

        return true;
    }

    // menu de navegación inferior
    private async void OnExplorarClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("//PantallaPrincipal");
    private async void OnPerfilClicked(object sender, EventArgs e) => await Shell.Current.GoToAsync("//PerfilPage");
}