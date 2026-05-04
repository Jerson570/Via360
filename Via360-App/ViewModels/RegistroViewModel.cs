using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.App.ViewModels
{
    public partial class RegistroViewModel : ObservableObject
    {
        // nombre segmentado
        [ObservableProperty] string primerNombre;
        [ObservableProperty] string segundoNombre;
        [ObservableProperty] string primerApellido;
        [ObservableProperty] string segundoApellido;

        [ObservableProperty] string email;
        [ObservableProperty] string password;

        // Propiedad para la confirmación
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegistrarCommand))]
        string confirmarPassword;

        [RelayCommand(CanExecute = nameof(CanRegister))]
        private async Task Registrar()
        {
            // 1. Validar el Segundo Apellido (Tu requerimiento)
            if (string.IsNullOrWhiteSpace(segundoApellido))
            {
                bool continuar = await Shell.Current.DisplayAlert("Atención",
                    "Has dejado el segundo apellido vacío. ¿Deseas continuar así?", "Sí", "No");
                if (!continuar) return;
            }

            // 2. Combinar el nombre completo (Tu requerimiento)
            string nombreCompleto = $"{primerNombre} {segundoNombre} {primerApellido} {segundoApellido}".Replace("  ", " ").Trim();

            // 3. Llamar al servicio (Esto lo haremos luego)
            // var exito = await _authService.RegisterAsync(Email, Password, nombreCompleto);
        }

        // Lógica que habilita/deshabilita el botón automáticamente
        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(primerNombre) &&
                   !string.IsNullOrWhiteSpace(primerApellido) &&
                   !string.IsNullOrWhiteSpace(email) &&
                   !string.IsNullOrWhiteSpace(password) &&
                   password == confirmarPassword; // Aquí ocurre la magia en tiempo real
        }
    }
}
