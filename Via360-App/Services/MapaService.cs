using Via360.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Via360.App.Services
{
    public class MapaService
    {
        public string GenerarHtml(List<IncidenteReporte> reportes)
        {
            // Creamos los marcadores de incidentes
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
            .leaflet-bottom {{ margin-bottom: 110px; }} 
        </style>
    </head>
    <body>
        <div id='map'></div>
        <script>
            // --- 1. CONFIGURACIÓN INICIAL DEL MAPA ---
            var map = L.map('map', {{ zoomControl: false }}).setView([6.17, -75.61], 15);
            
            L.tileLayer('https://{{s}}.tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png', {{
                attribution: '© OpenStreetMap', maxZoom: 19
            }}).addTo(map);

            L.control.zoom({{ position: 'bottomleft' }}).addTo(map);

            // --- 2. LÓGICA DE UBICACIÓN (LO NUEVO) ---
            var userMarker = null;

            // Esta función la llamaremos desde C# usando EvaluateJavaScriptAsync
            function actualizarUbicacionUsuario(lat, lon) {{
                if (userMarker) {{
                    userMarker.setLatLng([lat, lon]);
                }} else {{
                    userMarker = L.circleMarker([lat, lon], {{
                        radius: 9,
                        fillColor: '#4285F4',
                        color: 'white',
                        weight: 3,
                        opacity: 1,
                        fillOpacity: 1
                    }}).addTo(map).bindPopup('Tú estás aquí');
                }}
            }}

            function centrarEnUsuario() {{
                if (userMarker) {{
                    map.setView(userMarker.getLatLng(), 16);
                }}
            }}

            // --- 3. RENDERIZADO DE PINES DE INCIDENTES ---
            {string.Join("\n", pines)}

        </script>
    </body></html>";
        }
    }
}