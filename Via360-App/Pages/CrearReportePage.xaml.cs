using Via360.App.ViewModels;
using Via360.Shared.Models;

namespace Via360.App.Pages;

public partial class CrearReportePage : ContentPage
{
    public CrearReportePage(ReporteViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}