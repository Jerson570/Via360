using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Via360.Shared.Models;

namespace Via360.App.Services
{
    public class MapaService
    {
        public string GenerarHtml(List<Reporte> reportes, bool esAutoridad = false)
        {
            // 1. Filtramos los que NO tengan ubicación para evitar el crash
            var reportesValidos = reportes.Where(r => r != null && r.Ubicacion != null).ToList();

            var pines = reportesValidos.Select(r => {
                string lat = r.Ubicacion.Latitud.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string lon = r.Ubicacion.Longitud.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string tituloAmigable = ObtenerTextoAmigable(r.Tipo);
                string icono = ObtenerIcono(r.Tipo);
                string desc = r.Descripcion?.Replace("'", "\\'") ?? "Sin descripción";

                // LÓGICA DE COLOR DINÁMICA
                string colorBorde = "#512BD4"; // Morado estándar Ciudadano
                if (esAutoridad)
                {
                    colorBorde = r.Estado switch
                    {
                        EstadoReporte.Pendiente => "#FF3B30", // Rojo
                        EstadoReporte.EnProceso => "#FF9500", // Naranja
                        EstadoReporte.Resuelto => "#4CD964",  // Verde
                        _ => "#512BD4"
                    };
                }

                return $@"
        L.marker([{lat}, {lon}], {{
            icon: L.divIcon({{
                html: '<div style=""display:flex;justify-content:center;align-items:center;width:40px;height:40px;background:white;border-radius:50%;box-shadow:0 2px 5px rgba(0,0,0,0.3);border:3px solid {colorBorde};font-size:22px"">{icono}</div>',
                iconSize: [40,40], 
                className: ''
            }})
        }}).addTo(map)
           .bindPopup('<div style=""font-family:sans-serif""><b style=""color:{colorBorde};font-size:14px"">{tituloAmigable}</b><br><p style=""margin:5px 0"">{desc}</p><small style=""color:gray"">Estado: {r.Estado}</small></div>');";
            });

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
        private string ObtenerIcono(TipoIncidente tipo) => tipo switch
        {
            TipoIncidente.Accidente => "⚠️",
            TipoIncidente.SemáforoAveriado => "🚦", // Con tilde
            TipoIncidente.Bache => "🕳️",
            TipoIncidente.ObstrucciónVial => "🚧", // Con tilde
            TipoIncidente.ObraEnLaVía => "🏗️",    // Con tilde
            _ => "📍"
        };
        private string ObtenerTextoAmigable(Enum valor)
        {
            FieldInfo fi = valor.GetType().GetField(valor.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return valor.ToString(); // Fallback por si no tiene [Description]
        }
    }
}