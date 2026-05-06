using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Via360.Shared.Models;

namespace Via360.App.ViewModels;

public partial class AutoridadViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Reporte> reportes;

    public AutoridadViewModel()
    {
        Reportes = new ObservableCollection<Reporte>();
        CargarReportes();
    }

    private void CargarReportes()
    {
        // Inicializamos directamente con la clase correcta
        var lista = new List<Reporte>
    {
        new Reporte { IdReporte = "1", Descripcion = "Bache Crítico Calle 10", Fecha = DateTime.Now, Estado = EstadoReporte.Pendiente, Tipo = TipoIncidente.ObstrucciónVial },
            new Reporte { IdReporte = "2", Descripcion = "Semáforo Averiado", Fecha = DateTime.Now.AddDays(-1), Estado = EstadoReporte.Pendiente, Tipo = TipoIncidente.SemáforoAveriado },
            new Reporte { IdReporte = "3", Descripcion = "Grieta en Puente", Fecha = DateTime.Now.AddDays(-2), Estado = EstadoReporte.Resuelto, Tipo = TipoIncidente.ObraEnLaVía }
    };

        Reportes = new ObservableCollection<Reporte>(lista);
    }

    [RelayCommand]
    private async Task SeleccionarReporte(Reporte reporte)
    {
        if (reporte == null) return;

        // Navegación pasando el ID del reporte
        await Shell.Current.GoToAsync($"DetalleReporteAutoridadPage?id={reporte.IdReporte}");
    }
}