using FleetRegistryService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FleetRegistryService.Data.Repositories;

public sealed class HealthRepository(FleetRegistryDbContext dbContext) : IHealthRepository
{
    public async Task<bool> CanConnectAsync()
    {
        return await dbContext.Database.CanConnectAsync();
    }
}
