
## 📑 Índice
* [Arquitectura del Sistema](#-arquitectura-del-sistema)
* [Estructura del Proyecto](#-estructura-del-proyecto)
* [Decisiones Técnicas y Stack](#-decisiones-técnicas-y-stack)
* [Gestión de Datos](#-gestión-de-datos)

---

## 🏗️ Arquitectura del Sistema

El proyecto **Via360** está diseñado bajo una arquitectura desacoplada utilizando el patrón **MVVM (Model-View-ViewModel)**. Esta estructura garantiza una separación clara entre la interfaz de usuario y la lógica de negocio, permitiendo que el sistema sea escalable y fácil de mantener.

### Componentes de la Arquitectura:
*   **View (XAML):** Define la interfaz visual minimalista. Se comunica con el ViewModel exclusivamente mediante *Data Binding* y *Commands*.
*   **ViewModel:** Centraliza el estado de la aplicación, la lógica de navegación y la coordinación con los servicios.
*   **Services:** Capa de abstracción para funcionalidades específicas (como el motor de mapas), permitiendo que la lógica sea independiente de la plataforma.

---

## 📂 Estructura del Proyecto

| Módulo | Responsabilidad | Estructura Detallada |
| :--- | :--- | :--- |
| **Via360.App** | Capa de Presentación y UX | <details><summary>Explorar carpetas</summary><ul><li><b>Converters:</b> Implementación de <code>IValueConverter</code> para renderizado dinámico de HTML.</li><li><b>Pages:</b> Vistas XAML optimizadas para ciudadanos y autoridades.</li><li><b>ViewModels:</b> Gestión de estado reactivo y comandos de usuario.</li><li><b>Services:</b> Motor de mapas basado en Leaflet.js inyectado.</li></ul></details> |
| **Via360.Shared** | Núcleo de Datos (Dominio) | <details><summary>Explorar carpetas</summary><ul><li><b>Models:</b> Entidades puras (<code>Reporte</code>, <code>IncidenteReporte</code>).</li><li><b>Enums:</b> Definiciones de estados de gestión (<code>EstadoReporte</code>).</li></ul></details> |

---

## ⚙️ Decisiones Técnicas y Stack

Para el desarrollo de **Via360** se tomaron decisiones estratégicas priorizando la eficiencia del desarrollador y la experiencia del usuario final:

*   **Motor de Mapas Geoespaciales:** Se implementó **Leaflet.js** sobre soluciones nativas de Google Maps para garantizar la independencia de API Keys, reducir costos operativos y permitir una personalización total del diseño de los pines mediante CSS.
*   **Multiplataforma:** Uso de **.NET MAUI** para desplegar una base de código única en Android y Windows.
*   **Inyección de Contenido:** El mapa se renderiza mediante un `WebView` alimentado dinámicamente desde C#, permitiendo una integración fluida entre la lógica de .NET y las capacidades de visualización web de Leaflet.
*   **UI/UX:** Diseño basado en principios minimalistas, utilizando una paleta de colores coherente (`#512BD4`) y componentes visuales con bordes redondeados para una estética moderna.


