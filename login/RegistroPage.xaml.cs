using Firebase.Database;
using Firebase.Database.Query;
namespace login;

public partial class RegistroPage : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://via360-app-default-rtdb.firebaseio.com/");
    public RegistroPage()
	{
        InitializeComponent();
	}

    private async void OnFinalizarRegistroClicked(object sender, EventArgs e)
    {
        // 1. Verificación básica
        if (string.IsNullOrWhiteSpace(TxtNombre.Text) ||
            string.IsNullOrWhiteSpace(TxtEmail.Text) ||
            string.IsNullOrWhiteSpace(TxtPassword.Text) ||
            PickerRol.SelectedItem == null)
        {
            await this.DisplayAlertAsync("Atención", "Por favor completa todos los datos", "OK");
            return;
        }

        try
        {
            // 2. Creamos el objeto con los datos
            var nuevoUsuario = new Usuario
            {
                Nombre = TxtNombre.Text,
                Correo = TxtEmail.Text,
                Contrasena = TxtPassword.Text,
                Rol = PickerRol.SelectedItem.ToString()
            };

            // Enviar a firebase
            await client
                .Child("Usuarios")
                .PostAsync(nuevoUsuario);

            await this.DisplayAlertAsync("Vía360", "Registro exitoso en la base de datos", "Aceptar");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await this.DisplayAlertAsync("Error", "Fallo al conectar con Firebase: " + ex.Message, "OK");
        }
    }
}


public class Usuario
{
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Contrasena { get; set; }
    public string Rol { get; set; }
}
