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
        return NormalizeConnectionString(connectionString);
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

static string NormalizeConnectionString(string connectionString)
{
    if (!connectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) &&
        !connectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
    {
        return connectionString;
    }

    var uri = new Uri(connectionString);
    var userInfo = uri.UserInfo.Split(':', 2);

    var builder = new NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Database = uri.AbsolutePath.TrimStart('/'),
        Username = Uri.UnescapeDataString(userInfo[0]),
        Password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : null,
    };

    var query = uri.Query.TrimStart('?');
    foreach (var segment in query.Split('&', StringSplitOptions.RemoveEmptyEntries))
    {
        var parts = segment.Split('=', 2);
        var key = parts[0];
        var value = parts.Length > 1 ? Uri.UnescapeDataString(parts[1]) : null;

        if (string.IsNullOrEmpty(value))
            continue;

        switch (key.ToLowerInvariant())
        {
            case "sslmode":
                builder.SslMode = Enum.Parse<SslMode>(value, ignoreCase: true);
                break;
            case "connect_timeout":
                if (int.TryParse(value, out var timeout))
                    builder.CommandTimeout = timeout;
                break;
        }
    }

    return builder.ConnectionString;
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
