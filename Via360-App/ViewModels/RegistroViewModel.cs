using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Via360.App.Services;

namespace Via360.App.ViewModels
{
    public partial class RegistroViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly ApiService _apiService;

        // nombre segmentado

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegistrarCommand))]
        private string primerNombre;

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegistrarCommand))]
        private string segundoNombre;

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegistrarCommand))]
        private string primerApellido;

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegistrarCommand))]
        private string segundoApellido;

        //credenciales

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegistrarCommand))]
        private string email;

        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegistrarCommand))]
        private string password;

        // propiedad para la confirmación
        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(RegistrarCommand))]
        private string confirmarPassword;


        //CONSTRUCTOR

        public RegistroViewModel(IAuthService authService, ApiService apiService)
        {
            _authService = authService;
            _apiService = apiService;

        }
        [RelayCommand(CanExecute = nameof(CanRegister))]
        private async Task Registrar()
        {
            // Validar el Segundo Apellido
            if (string.IsNullOrWhiteSpace(segundoApellido))
            {
                bool continuar = await Shell.Current.DisplayAlert("Atención",
                    "Has dejado el segundo apellido vacío. ¿Deseas continuar así?", "Sí", "No");
                if (!continuar) return;
            }

            // Unificación de nombre
            var nombres = new[] { PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido };
            string nombreCompleto = string.Join(" ", nombres.Where(n => !string.IsNullOrWhiteSpace(n))).Trim();
            try
            {
                // Firebase Auth
                var uid = await _authService.RegistroAsync(Email, Password, nombreCompleto);
                if (string.IsNullOrEmpty(uid))
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo registrar el usuario. Intenta nuevamente.", "OK");
                    return;
                }
                // Backend Azure (Ajustado al contrato real de la API)
                var usuarioParaBackend = new
                {
                    IdUsuario = uid,
                    Nombre = nombreCompleto,
                    Email, 
                    FechaRegistro = DateTime.UtcNow,
                    Cargo = (string)null,
                    Entidad = (string)null
                };


                bool exitoBackend = await _apiService.RegistrarCiudadanoEnBackend(usuarioParaBackend);
                if (exitoBackend)
                {
                    await Shell.Current.DisplayAlert("Éxito", "Usuario creado y guardado.", "Aceptar");
                    await Shell.Current.GoToAsync(".."); // Regresa a la pantalla anterior
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error de Perfil", "Cuenta creada pero falló el registro en el servidor.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error Crítico", $"Fallo en la operación: {ex.Message}", "OK");
            }
        }

        // Lógica que habilita/deshabilita el botón automáticamente
        private bool CanRegister()
        {
            // El Toolkit genera automáticamente "PrimerNombre" a partir de "primerNombre"
            return !string.IsNullOrWhiteSpace(PrimerNombre) &&
                   !string.IsNullOrWhiteSpace(PrimerApellido) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password == ConfirmarPassword;
        }
    }
}
