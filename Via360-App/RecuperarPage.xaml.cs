using System.Text.RegularExpressions;
namespace Via360.App;

public partial class RecuperarPage : ContentPage
{
	public RecuperarPage()
	{
		InitializeComponent();
	}
    private async void OnEnviarCodigoClicked(object sender, EventArgs e)
    {
        // Obtener el correo 
        string email = TxtEmailRecuperar?.Text ?? string.Empty;

        
        if (string.IsNullOrWhiteSpace(email))
        {
            await this.DisplayAlertAsync("Atención", "Por favor, ingresa tu correo electrónico.", "OK");
            return;
        }

        // formato de correo
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(email, emailPattern))
        {
            await this.DisplayAlertAsync("Formato Inválido", "Por favor, ingresa un correo electrónico real.", "OK");
            return;
        }

        // conexion futura para envio de codigos
        await this.DisplayAlertAsync("Código Enviado",
            $"Hemos enviado las instrucciones para restablecer tu contraseña a: {email}",
            "Entendido");

        
        await Shell.Current.GoToAsync("..");
    }
}