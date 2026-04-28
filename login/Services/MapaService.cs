using login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace login.Services
{
    internal class MapaService
    {
        public string GenerarHtml(List<IncidenteReporte> reportes)
        {
            var pines = reportes.Select(r => $@"
        L.marker([{r.Latitud.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                   {r.Longitud.ToString(System.Globalization.CultureInfo.InvariantCulture)}], {{
            icon: L.divIcon({{
                html: '<div style=""font-size:22px"">{r.IconoTipo ?? "📍"}</div>',
                iconSize: [30,30], className: ''
            }})
        }}).addTo(map)
          .bindPopup('<b>{r.IconoEstado ?? "⚪"} {(r.Tipo ?? "INCIDENTE").ToUpper()}</b><br>{r.Descripcion ?? "Sin descripción"}<br><small>👍 {r.Votos} votos</small>');
    ");

            return $@"<!DOCTYPE html><html>
    <head>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <link rel='stylesheet' href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css'/>
        <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>
        <style>
            body{{margin:0;padding:0}} 
            #map{{width:100vw;height:100vh}}
            /* Ajuste visual para que el zoom no quede pegado a la barra de navegación */
            .leaflet-bottom {{ margin-bottom: 110px; }} 
        </style>
    </head>
    <body><div id='map'></div>
    <script>
        // 1. Inicializamos el mapa DESACTIVANDO el zoom por defecto (zoomControl: false)
        var map = L.map('map', {{ zoomControl: false }}).setView([6.2314, -75.6148], 15);
        
        L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
            attribution: '© OpenStreetMap', maxZoom: 19
        }}).addTo(map);

        // 2. Agregamos el control de zoom manualmente en la esquina inferior izquierda
        L.control.zoom({{ position: 'bottomleft' }}).addTo(map);

        {string.Join("\n", pines)}
    </script></body></html>";
        }
    }
}
