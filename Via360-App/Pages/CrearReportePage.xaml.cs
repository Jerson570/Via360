using Via360.Shared.Models;

namespace Via360.App.Pages;

public partial class CrearReportePage : ContentPage
{
	public CrearReportePage()
	{
		InitializeComponent();
        
        CargarCategorias();
    }
    private async void OnPublicarClicked(object sender, EventArgs e)
    {
        // Validar que no estén vacíos
        if (PickerTipo.SelectedIndex == -1 || string.IsNullOrWhiteSpace(EditorDescripcion.Text))
        {
            await this.DisplayAlertAsync("Atención", "Completa todos los campos antes de publicar.", "OK");
            return;
        }


        // Por ahora simulamos coordenadas, luego usaremos el GPS real
        var ubicacionReporte = new Ubicacion(6.2442, -75.5812, "Ubicación manual");

        // Crear el objeto reporte
       
        var nuevoReporte = new Reporte(
            IdUsuario: "ID_USUARIO_TEMPORAL", 
            tipoInicial: (TipoIncidente)PickerTipo.SelectedIndex,
            descripcion: EditorDescripcion.Text,
            ubicacion: ubicacionReporte,
            estadoInicial: EstadoReporte.Pendiente
        );

        //  Mostrar confirmación
        await this.DisplayAlertAsync("Éxito", $"Has reportado un {nuevoReporte.Tipo}. ¡Gracias!", "OK");

        //  Cerrar la ventana y volver al mapa
        await Navigation.PopModalAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    private void CargarCategorias()
    {

        var mejoresOpciones = new List<string>
    {
        "Accidente de Tránsito",    
        "Semáforo Averiado",        
        "Bache o Hueco",            
        "Obstrucción en la Vía",    
        "Obra en la Vía",           
        "Otro Incidente"           
    };


        PickerTipo.ItemsSource = mejoresOpciones;
    }
}