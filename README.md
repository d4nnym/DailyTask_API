# Configuración de Nuevo Proyecto Asp.Net Core

## Instalaciones Previas Necesarias

1. Abrir la terminal e instalar dotnet-ef

```bash
dotnet tool install --global dotnet-ef

dotnet ef --version
```

## Configuración de Swagger

### Instalación de Swagger

1. Clic derecho en nuestro Proyecto 
2. Manage NuGet Packages
3. Busca e instala: **Swashbuckle.AspNetCore**

### Configurar `Program.cs` dentro de nuestro proyecto

Mantiene OpenApi y agrega Swagger UI

```csharp
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// OpenAPI nativo (JSON)
builder.Services.AddOpenApi();

// Swagger (UI + generator)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // OpenAPI JSON (nativo)
    app.MapOpenApi(); // => /openapi/v1.json (según tu plantilla)

    // Swagger JSON + UI
    app.UseSwagger();      // => /swagger/v1/swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DailyTask API v1");
        c.RoutePrefix = "swagger"; // => /swagger
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
```

## Estructura del Proyecto (**Clean Architecture**)

**Clean Architecture** y **DDD (Domain‑Driven Design)**. Es una forma de organizar el código para que sea **modular, mantenible y escalable**, separando responsabilidades de manera estricta.

### ¿Qué tipo de arquitectura es?

Es una **arquitectura por capas desacopladas**, donde:

- La **lógica de negocio** vive en el *Domain*
- La **implementación técnica** vive en *Infrastructure*
- La **exposición al usuario o clientes** vive en *App* y *Api*

Este patrón se usa muchísimo en ASP.NET Core porque permite crecer sin que el proyecto se vuelva un caos.

- →📁 ProjectName (Solution)
  - ↳📁 ProjectName.Api
  - ↳📁 ProjectName.Application
  - ↳📁 ProjectName.Domain
  - ↳📁ProjectName.Infrastructure

# Referencias correctas

Configura las dependencias así:

- `DailyTask.Application` → referencia a `DailyTask.Domain`
- `DailyTask.Infrastructure` → referencia a `DailyTask.Domain` y `DailyTask.Application`
- `DailyTask.Api` → referencia a `DailyTask.Application` y `DailyTask.Infrastructure`

## Instalar paquetes NuGet necesarios

1. En `DailyTask.Infrastructure` instala:
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Oracle.EntityFrameworkCore
    - Oracle.ManagedDataAccess.Core
2. En `DailyTask.Api` instala:
    - Microsoft.EntityFrameworkCore.Design
3. Otra forma de instalarlos desde CMD:

```bash

dotnet add Carpeta package NombreDePaqueteNugGet --version 10.x.x
# --version debe ser la misma para todos los paquetes

dotnet add DailyTask.Api package Microsoft.EntityFrameworkCore.Design --version 10.0.3
```

## API: registrar DbContext + connection string

En `DailyTask.Api/appsettings.Development.json` agrega:

```json
{
  "ConnectionStrings": {
    "Oracle": "User Id=SCHEMA;Password=Password;Data Source=localhost:1521/MYATP_medium.adb.oraclecloud.com;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

## Swagger UI

El final, ejecuta el proyecto htto y accede a localhost:5149/swagger para obtener los métodos construidos en tu aplicación

<img width="1488" height="910" alt="image" src="https://github.com/user-attachments/assets/65e80c5a-d3e3-46a0-86ee-978a47dc8c46" />
