using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Via360.App.Services;
using Via360.App.ViewModels;
using Via360.Shared.Models;

namespace Via360.App.Pages.Autoridad;

public partial class AutoridadPage : ContentPage
{
    private readonly AutoridadViewModel _viewModel;


    public AutoridadPage(AutoridadViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    [RelayCommand]
    public async Task SeleccionarReporte(Reporte reporte)
    {
        if (reporte == null) return;

        // Navegamos a la página de detalle pasando el objeto reporte
        var navigationParameter = new Dictionary<string, object>
    {
        { "ReporteSeleccionado", reporte }
    };

        await Shell.Current.GoToAsync("DetalleReporteAutoridadPage", navigationParameter);
    }


    


}