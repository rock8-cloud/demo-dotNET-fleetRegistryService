using Microsoft.EntityFrameworkCore;

namespace FleetRegistryService.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FleetRegistryDbContext>();

        await dbContext.Database.ExecuteSqlRawAsync("create schema if not exists fleet_registry;");
        await dbContext.Database.MigrateAsync();
    }
}
