using FleetRegistryService.Domain;
using FleetRegistryService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FleetRegistryService.Data.Repositories;

public sealed class CaptainRepository(FleetRegistryDbContext dbContext) : ICaptainRepository
{
    public async Task<IReadOnlyList<Captain>> GetAllAsync()
    {
        return await dbContext.Captains
            .AsNoTracking()
            .OrderBy(captain => captain.Id)
            .Select(captain => new Captain(
                captain.Id,
                captain.Name,
                captain.Rank))
            .ToListAsync();
    }
}
