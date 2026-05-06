using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Via360.App.Pages;
using Via360.App.Services;

namespace Via360.App.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly ApiService _apiService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isBusy;

        public LoginViewModel(IAuthService authService, ApiService apiService)
        {
            _authService = authService;
            _apiService = apiService;
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

            //autenticación firebase
            var uid = await _authService.LoginAsync(Email, Password);

            if (uid != null)
            {
                //validacion de rol
                var rol = await _apiService.ObtenerRolUsuario(uid);
                IsBusy = false;
                // Navegación a la página principal

                if (rol == "Autoridad")
                {
                    await Shell.Current.GoToAsync($"//{nameof(PantallaPrincipalAutoridad)}");
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(PantallaPrincipal)}");
                }
            }
            else
            {
                IsBusy = false;
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