using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Via360.App.Services;
using Via360.Shared.Models;

namespace Via360.App.ViewModels;

public partial class AutoridadViewModel : ObservableObject
{

    [RelayCommand]
    private async Task SeleccionarReporte(Reporte reporte)
    {
        if (reporte == null) return;

        // Navegación pasando el ID del reporte
        await Shell.Current.GoToAsync($"DetalleReporteAutoridadPage?id={reporte.IdReporte}");
    }

    [RelayCommand]
    public async Task Logout()
    {
        bool confirmar = await Shell.Current.DisplayAlert(
            "Cerrar Sesión",
            "¿Estás seguro de que quieres salir?",
            "Sí, salir",
            "Cancelar");

        if (confirmar)
        {
            // El "//" es vital para resetear la navegación
            await Shell.Current.GoToAsync("//PantallaPrincipal");
        }
    }


}