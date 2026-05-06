using System.Collections.ObjectModel;
using Via360.Shared.Models;

namespace Via360.App.Pages;

public partial class MisReportesPage : ContentPage
{
    public MisReportesPage(MisReportesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // MAUI lo ejecuta solo cada vez que entras a la pestaña.
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Verificamos que el BindingContext sea el correcto y ejecutamos
        if (BindingContext is MisReportesViewModel viewModel)
        {
            await viewModel.CargarReportesCommand.ExecuteAsync(null);
        }
    }
}