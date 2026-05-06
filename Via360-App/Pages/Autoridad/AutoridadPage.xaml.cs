using Via360.App.ViewModels;

namespace Via360.App.Pages.Autoridad;

public partial class AutoridadPage : ContentPage
{
    public AutoridadPage(AutoridadViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // El método OnReporteSelected desaparece, ahora lo maneja el Command
}