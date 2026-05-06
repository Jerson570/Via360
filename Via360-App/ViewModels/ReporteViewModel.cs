using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Via360.App.Services;
using Via360.Shared.Models; // Para la clase Location y Geolocation

namespace Via360.App.ViewModels
{
    public partial class ReporteViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly IAuthService _authService;

        public ReporteViewModel(ApiService apiService, IAuthService authService)
        {
            _apiService = apiService;
            _authService = authService;

            // inicialización lista 
            ListaNombresTipos = _mapaTipos.Keys.ToList();
        }

        // --- Propiedades bindeables ---

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PublicarCommand))]
        private string descripcion;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PublicarCommand))]
        private TipoIncidente? tipoSeleccionado;

        [ObservableProperty]
        private bool isBusy;

        // --- Diccionario de Mapeo para Picker ---
        private readonly Dictionary<string, TipoIncidente> _mapaTipos = new()
        {
            { "Accidente de Tránsito", TipoIncidente.Accidente },
            { "Semáforo Averiado", TipoIncidente.SemáforoAveriado },
            { "Bache o Hueco", TipoIncidente.Bache },
            { "Obstrucción en la Vía", TipoIncidente.ObstrucciónVial },
            { "Obra en la Vía", TipoIncidente.ObraEnLaVía },
            { "Otro Incidente", TipoIncidente.Otro }
        };

        // lista que lee el picker
        public List<string> ListaNombresTipos { get; }

        // guarda el texto que el usuario toque en el celular
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PublicarCommand))]
        private string nombreTipoSeleccionado;

        // --- Comandos ---

        [RelayCommand]
        private async Task TomarFoto()
        {
            // NO IMPLEMENTADO
        }

        [RelayCommand(CanExecute = nameof(CanPublish))]
        private async Task Publicar()
        {
            if (IsBusy) return;

            if (!_mapaTipos.TryGetValue(NombreTipoSeleccionado, out var tipoReal))
            {
                tipoReal = TipoIncidente.Otro; // Valor por defecto si no se encuentra
                return;
            }

            try
            {
                IsBusy = true;

                // 1. Obtener el UID REAL
                var userId = await _authService.GetActualUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    await Shell.Current.DisplayAlert("Error", "No se detectó una sesión activa", "OK");
                    return;
                }

                // 2. GPS Real
                var location = await ObtenerUbicacionActual();
                if (location == null)
                {
                    await Shell.Current.DisplayAlert("GPS", "No se pudo obtener la ubicación. Activa el GPS.", "OK");
                    return;
                }

                // 3. Construcción del objeto (Sin hardcoding)
                var nuevoReporte = new Reporte(
                    IdUsuario: userId, // <--- CAMBIADO: Usamos la variable recuperada
                    tipoInicial: tipoReal,
                    descripcion: Descripcion,
                    ubicacion: new Ubicacion(location.Latitude, location.Longitude, "Capturado por GPS"),
                    estadoInicial: EstadoReporte.Pendiente,
                    UrlImagen: null
                );

                // 4. Envío a Azure
                bool exito = await _apiService.GuardarReporte(nuevoReporte);

                if (exito)
                {
                    await Shell.Current.DisplayAlert("Éxito", "Incidente reportado correctamente", "Aceptar");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Azure rechazó el reporte. Revisa los logs.", "OK");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en Publicar: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Fallo crítico de conexión", "Cerrar");
            }
            finally { IsBusy = false; }
        }

        private async Task<Location> ObtenerUbicacionActual()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (status != PermissionStatus.Granted) return null;

            return await Geolocation.Default.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10)));
        }

        private bool CanPublish() => !string.IsNullOrWhiteSpace(Descripcion) && !string.IsNullOrEmpty(NombreTipoSeleccionado) && !IsBusy;

        [RelayCommand]
        private async Task Cancelar() => await Shell.Current.GoToAsync("..");
    }
}