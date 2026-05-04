using Via360.App.ViewModels;

namespace Via360.App;

public partial class RegistroPage : ContentPage
{
    public RegistroPage(RegistroViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}