using Via360.App.ViewModels;

namespace Via360.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}