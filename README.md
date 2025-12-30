# Documentación de la Estructura del Proyecto - FiscalFlow

Este proyecto utiliza el enfoque de arquitectura limpia para organizar el código, facilitando la separación de responsabilidades, la escalabilidad y el mantenimiento. A continuación, se describe la estructura de carpetas y archivos del proyecto.

## **Capas del Proyecto**

### **1. Application**
Contiene la lógica de aplicación y define las interfaces, DTOs y servicios necesarios para interactuar con otras capas.

- **DTOs**: Objetos de transferencia de datos utilizados para encapsular y transportar datos entre capas.
- **Interfaces**: Contratos que definen la lógica que deben implementar las clases concretas.
- **Models**: Modelos que representan estructuras utilizadas en el contexto de la lógica de aplicación.
- **Resources**: Archivos de recursos como cadenas localizadas o configuraciones específicas.
- **Services**: Servicios de aplicación que implementan la lógica específica del negocio.
- **DependencyInjection.cs**: Configuración para registrar los servicios de Application en el contenedor de dependencias.
- **GlobalUsings.cs**: Archivo para declarar los using globales que simplifican las referencias en esta capa.

---

### **2. Domain**
Representa el núcleo del negocio y contiene las entidades, valores constantes y enumeraciones.

- **Constants**: Valores constantes que son utilizados en toda la aplicación.
- **Entities**: Clases que representan las entidades del dominio con sus propiedades y comportamientos.
- **Enums**: Enumeraciones que representan conjuntos de valores predefinidos.
- **GlobalUsings.cs**: Archivo para declarar los using globales que simplifican las referencias en esta capa.

---

### **3. Infrastructure**
Proporciona implementaciones concretas para las interfaces definidas en `Application`. Incluye servicios para correo electrónico, identidad, logging, persistencia y más.

- **Email**: Lógica relacionada con el envío de correos electrónicos.
- **Identity**: Manejo de autenticación y autorización.
- **Logging**: Configuración y servicios relacionados con el registro de eventos.
- **Persistence**:
  - **Data**: Contiene el `DbContext` para interactuar con la base de datos.
  - **Repositories**: Implementaciones de repositorios para acceder a los datos.
- **DependencyInjection.cs**: Configuración para registrar los servicios de Infrastructure en el contenedor de dependencias.
- **GlobalUsings.cs**: Archivo para declarar los using globales que simplifican las referencias en esta capa.

---

### **4. Api**
Proyecto ASP.NET Core en .NET 10 que actúa como punto de entrada de la aplicación. Incluye controladores, middlewares, y configuraciones específicas para la interacción con los usuarios y servicios externos.

---

## **Consideraciones Generales**
- La separación en capas asegura una alta cohesión dentro de cada capa y un bajo acoplamiento entre ellas.
- `DependencyInjection.cs` en cada capa se utiliza para registrar sus servicios específicos en el contenedor de dependencias global de ASP.NET Core.
- `GlobalUsings.cs` simplifica la gestión de espacios de nombres en los archivos de cada capa.

Esta estructura permite que el código sea modular, testable y fácilmente extensible, facilitando la colaboración en equipos grandes y el mantenimiento a largo plazo.

# Documentación

## Uso de Scaffold-DbContext
El comando `Scaffold-DbContext` se utiliza en proyectos basados en Entity Framework Core para generar automáticamente las clases de entidad y el contexto de base de datos a partir de una base de datos existente. Este proceso es útil cuando se adopta un enfoque de desarrollo basado en la base de datos.

### **Comando Utilizado**
```bash
Scaffold-DbContext 'Name=DefaultConnection' Microsoft.EntityFrameworkCore.SqlServer -OutputDir ../FiscalFlow.Domain/Entities -ContextDir ../FiscalFlow.Infrastructure/Persistence/Data -Context AppDbContext -Force 


```

