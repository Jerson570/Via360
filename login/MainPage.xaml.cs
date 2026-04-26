using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
namespace login
{

    public partial class MainPage : ContentPage
    {
        private bool esAutoridad = false;
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnRoleChanged(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null || BtnRoleAutoridad == null || BtnRoleCiudadano == null || StackRegistro == null) return;

            if (button == BtnRoleAutoridad)
            {
                esAutoridad = true;
                BtnRoleAutoridad.BackgroundColor = Color.FromArgb("#512BD4");
                BtnRoleAutoridad.TextColor = Colors.White;
                BtnRoleCiudadano.BackgroundColor = Colors.Transparent;
                BtnRoleCiudadano.TextColor = Color.FromArgb("#512BD4");

                StackRegistro.Opacity = 0;
                StackRegistro.InputTransparent = true;
            }
            else
            {
                esAutoridad = false;
                BtnRoleCiudadano.BackgroundColor = Color.FromArgb("#512BD4");
                BtnRoleCiudadano.TextColor = Colors.White;
                BtnRoleAutoridad.BackgroundColor = Colors.Transparent;
                BtnRoleAutoridad.TextColor = Color.FromArgb("#512BD4");

                StackRegistro.Opacity = 1;
                StackRegistro.InputTransparent = false;
            }
        }

        // Funcion iniciar sesion
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = TxtEmail?.Text ?? "";
            string password = TxtPassword?.Text ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await this.DisplayAlertAsync("Atención", "Por favor, completa todos los campos.", "OK");
                return;
            }

            // primera validacion
            string rol = esAutoridad ? "Autoridad" : "Ciudadano";
            await this.DisplayAlertAsync("Vía360", $"Iniciando sesión como {rol}...", "Aceptar");
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            
            await Shell.Current.GoToAsync(nameof(RecuperarPage));
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
           
            await Shell.Current.GoToAsync(nameof(RegistroPage));
        }

    }
    
}
