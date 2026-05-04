using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Via360.App.Pages;
using Via360.App.Services;

namespace Via360.App.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isBusy;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        public async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor llena todos los campos.", "OK");
                return;
            }

            IsBusy = true;
            var uid = await _authService.LoginAsync(Email, Password);
            IsBusy = false;

            if (uid != null)
            {
                // Navegación a la página que acabas de crear/registrar
                await Shell.Current.GoToAsync($"//{nameof(PerfilPage)}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Credenciales incorrectas o problema de conexión.", "OK");
            }
        }

        [RelayCommand]
        public async Task NavegarRegistro()
        {
            // se usa nameof para evitar errores de escritura
            await Shell.Current.GoToAsync("RegistroPage");
        }

        [RelayCommand]
        public async Task NavegarRecuperar()
        {
            await Shell.Current.GoToAsync("RecuperarPage");
        }
    }
}