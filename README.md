# Via360 🚗
**Sistema Inteligente de Gestión Vial y Reporte Ciudadano**

Via360 es una aplicación multiplataforma desarrollada con **.NET MAUI** diseñada para mejorar la infraestructura urbana. Permite a los ciudadanos reportar incidentes viales (baches, semáforos averiados, grietas) en tiempo real, mientras que las autoridades disponen de un panel de control para gestionar y priorizar estas reparaciones.

## Características Principales

*   **Mapa Interactivo:** Integración de **OpenStreetMap** mediante **Leaflet.js** para una visualización fluida y sin costos de API.
*   **Reporte Ciudadano:** Interfaz intuitiva para capturar la ubicación exacta y el tipo de incidente.
*   **Panel de Autoridad:** Vista especializada para la gestión de estados (Pendiente, En Proceso, Resuelto).
*   **Arquitectura MVVM:** Código limpio y mantenible utilizando el **CommunityToolkit.Mvvm**.
*   **Diseño Minimalista:** Estética moderna con paletas de colores enfocadas en la legibilidad y la experiencia de usuario.

## Stack Tecnológico

*   **Frontend:** .NET MAUI (C# / XAML)
*   **Mapas:** Leaflet.js & OpenStreetMap
*   **Lógica de Negocio:** MVVM Pattern
*   **Modelos Compartidos:** Librería de clases .NET Standard para comunicación cliente-servidor.



## 📂 Estructura del Proyecto

| Módulo | Responsabilidad | Estructura Detallada |
| :--- | :--- | :--- |
| **Via360.App** | Capa de Presentación y UX | <details><summary>Explorar carpetas</summary><ul><li><b>Converters:</b> Implementación de <code>IValueConverter</code> para renderizado dinámico de HTML.</li><li><b>Pages:</b> Vistas XAML optimizadas para ciudadanos y autoridades.</li><li><b>ViewModels:</b> Gestión de estado reactivo y comandos de usuario.</li><li><b>Services:</b> Motor de mapas basado en Leaflet.js inyectado.</li></ul></details> |
| **Via360.Shared** | Núcleo de Datos (Dominio) | <details><summary>Explorar carpetas</summary><ul><li><b>Models:</b> Entidades puras (<code>Reporte</code>, <code>IncidenteReporte</code>).</li><li><b>Enums:</b> Definiciones de estados de gestión (<code>EstadoReporte</code>).</li></ul></details> |





