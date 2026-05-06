using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Via360.App.Pages.Autoridad;

public partial class DetalleReporteAutoridadPage : ContentPage
{
	public DetalleReporteAutoridadPage()
	{
		InitializeComponent();
	}
    /*
    [QueryProperty(nameof(IdRecibido), "id")]
    public partial class DetalleReporteAutoridadViewModel : ObservableObject
    {
        [ObservableProperty]
        private string idRecibido;

        // Aquí buscarías el reporte por ese ID...
    }
    */
    /*[RelayCommand]
    public async Task CambiarEstado()
    {
        // Esto muestra un menú emergente nativo y elegante
        string accion = await Shell.Current.DisplayActionSheet(
         "Cancelar", null,"Pendiente", "En Proceso", "Resuelto");

        if (accion != "Cancelar" && accion != null)
        {
            // Aquí llamarías a tu ApiService para actualizar el estado en Azure
            // Por ahora, lo actualizamos localmente para ver el cambio
            ReporteSeleccionado.Estado = accion;

            await Shell.Current.DisplayAlert("Éxito", $"El reporte ahora está: {accion}", "OK");
        }
    }*/
}