using DailyTask.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DailyTask.Infrastructure.Services;
using DailyTask.App.Services;
using DailyTask.App.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// OpenAPI nativo (JSON)
builder.Services.AddOpenApi();

// Swagger (UI + generator)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddDbContext<DailyTaskDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("Oracle");
    options.UseOracle(cs);
});

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

/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();*/
