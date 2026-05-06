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
        private List<Reporte> _reportesMaestros = new();

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
                await Task.Delay(800, _cts.Token); // Debounce para no ametrallar la API

                // Llamamos a tu ApiService que ya devuelve List<Reporte>
                var nuevos = await _apiService.ObtenerReportesCercanosAsync();
                System.Diagnostics.Debug.WriteLine($"Reportes cargados: {nuevos.Count}");

                _reportesMaestros = nuevos ?? new List<Reporte>();

                // Esta línea es la que dispara el GenerarHtml y actualiza el WebView
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
                : _reportesMaestros.Where(r => r.Tipo.ToString().ToLower() == FiltroActual.ToLower()).ToList();

            HtmlMapa = _mapaService.GenerarHtml(filtrados);
        }
    }
}
