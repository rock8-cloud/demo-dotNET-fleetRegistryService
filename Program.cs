using FleetRegistryService.Data;
using FleetRegistryService.Data.Repositories;
using FleetRegistryService.Repositories;
using FleetRegistryService.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
    var connectionString = GetConfigurationValue(configuration, "CONNECTION_STRING");
    if (!string.IsNullOrWhiteSpace(connectionString))
    {
        return connectionString;
    }

    var host = GetConfigurationValue(configuration, "POSTGRES_HOST") ?? "localhost";
    var port = int.TryParse(GetConfigurationValue(configuration, "POSTGRES_PORT"), out var configuredPort)
        ? configuredPort
        : 5432;
    var database = GetConfigurationValue(configuration, "POSTGRES_DB") ?? "orbital_operations";
    var username = GetConfigurationValue(configuration, "POSTGRES_USER") ?? "postgres";
    var password = GetConfigurationValue(configuration, "POSTGRES_PASSWORD") ?? "postgres";

    return new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = port,
        Database = database,
        Username = username,
        Password = password
    }.ConnectionString;
}

static string? GetConfigurationValue(IConfiguration configuration, string key)
{
    var value = configuration[key];
    if (!string.IsNullOrWhiteSpace(value))
    {
        return value.Trim();
    }

    var filePath = configuration[$"{key}_FILE"];
    if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
    {
        return null;
    }

    return File.ReadAllText(filePath).Trim();
}
