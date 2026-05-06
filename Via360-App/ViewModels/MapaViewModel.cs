using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using Via360.App.Services;
using Via360.Shared.Models;

namespace Via360.App.ViewModels
{
    public partial class MapaViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly MapaService _mapaService;
        private CancellationTokenSource _cts;

        // Lista maestra (lo que descargamos de Azure)
        private List<IncidenteReporte> _reportesMaestros = new();

        [ObservableProperty]
        private string htmlMapa;

        [ObservableProperty]
        private string filtroActual = "todos";

        public MapaViewModel(ApiService apiService, MapaService mapaService)
        {
            _apiService = apiService;
            _mapaService = mapaService;
        }

        // Solo descarga si el movimiento es grande
        public async Task CargarReportesAsync(double lat, double lon)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            try
            {
                await Task.Delay(800, _cts.Token); // Debounce de 800ms

                // Pedimos a Azure (Solo pendientes/en proceso para ciudadanos)
                //var nuevos = await _apiService.ObtenerReportesCercanos(lat, lon);

                //_reportesMaestros = nuevos.ToList();
                ActualizarMapa();
            }
            catch (OperationCanceledException) { }
        }

        public void AplicarFiltroLocal(string tipo)
        {
            FiltroActual = tipo;
            ActualizarMapa();
        }

        private void ActualizarMapa()
        {
            var filtrados = FiltroActual == "todos"
                ? _reportesMaestros
                : _reportesMaestros.Where(r => r.Tipo == FiltroActual).ToList();

            HtmlMapa = _mapaService.GenerarHtml(filtrados);
        }
    }
}
