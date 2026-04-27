using FleetRegistryService.Data;
using FleetRegistryService.Data.Repositories;
using FleetRegistryService.Repositories;
using FleetRegistryService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<FleetRegistryDbContext>(options =>
    options.UseNpgsql(
        GetConnectionString(builder.Configuration),
        npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "fleet_registry")));
builder.Services.AddScoped<IFleetService, FleetService>();
builder.Services.AddScoped<ICaptainService, CaptainService>();
builder.Services.AddScoped<IHealthService, HealthService>();
builder.Services.AddScoped<IFleetRepository, FleetRepository>();
builder.Services.AddScoped<ICaptainRepository, CaptainRepository>();
builder.Services.AddScoped<IHealthRepository, HealthRepository>();

var app = builder.Build();

await DatabaseInitializer.InitializeAsync(app.Services);

app.MapControllers();

app.Run();

static string GetConnectionString(IConfiguration configuration)
{
    var connectionString = configuration["CONNECTION_STRING"];
    if (!string.IsNullOrWhiteSpace(connectionString))
    {
        return connectionString;
    }

    var host = configuration["POSTGRES_HOST"] ?? "localhost";
    var port = configuration["POSTGRES_PORT"] ?? "5432";
    var database = configuration["POSTGRES_DB"] ?? "orbital_operations";
    var username = configuration["POSTGRES_USER"] ?? "postgres";
    var password = configuration["POSTGRES_PASSWORD"] ?? "postgres";

    return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
}
