namespace Via360.App.Pages;

public partial class PerfilPage : ContentPage
{
	public PerfilPage()
	{
		InitializeComponent();
        CargarDatosUsuario();
      
	}
    private void CargarDatosUsuario()
     {
         // Por ahora son estáticos, luego los traeremos de tu App.CurrentUser o Preferences
         LblNombreUsuario.Text = "Inge";
         LblCorreoUsuario.Text = "inge@estudiante.com";
     }

    
    private async void OnCerrarSesionClicked(object sender, EventArgs e)
    {
        bool answer = await this.DisplayAlertAsync("Cerrar Sesión", "¿Estás seguro de que quieres salir?", "Sí", "No");
        if (answer)
        {
            // Aquí borrarías los datos de sesión y mandarías al Login
            await Shell.Current.GoToAsync("//MainPage");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Esto te regresa a la ruta anterior o a la principal
        await Shell.Current.GoToAsync(".."); 
    }
}