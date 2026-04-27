using FleetRegistryService.Data.Entities;
using FleetRegistryService.Domain;
using FleetRegistryService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FleetRegistryService.Data.Repositories;

public sealed class FleetRepository(FleetRegistryDbContext dbContext) : IFleetRepository
{
    public async Task<IReadOnlyList<Spacecraft>> GetAllAsync()
    {
        return await dbContext.Spacecraft
            .AsNoTracking()
            .OrderBy(spacecraft => spacecraft.Id)
            .Select(spacecraft => new Spacecraft(
                spacecraft.Id,
                spacecraft.Name,
                spacecraft.Type,
                spacecraft.Capacity,
                spacecraft.Status))
            .ToListAsync();
    }

    public async Task<Spacecraft?> GetByIdAsync(int id)
    {
        var spacecraft = await dbContext.Spacecraft
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);

        return spacecraft is null ? null : ToDomain(spacecraft);
    }

    public async Task<Spacecraft> AddAsync(Spacecraft spacecraft)
    {
        var entity = new SpacecraftEntity
        {
            Name = spacecraft.Name,
            Type = spacecraft.Type,
            Capacity = spacecraft.Capacity,
            Status = spacecraft.Status
        };

        dbContext.Spacecraft.Add(entity);
        await dbContext.SaveChangesAsync();

        return ToDomain(entity);
    }

    public async Task<IReadOnlyList<Spacecraft>> GetByStatusAsync(string status)
    {
        return await dbContext.Spacecraft
            .AsNoTracking()
            .Where(spacecraft => spacecraft.Status == status)
            .OrderBy(spacecraft => spacecraft.Id)
            .Select(spacecraft => new Spacecraft(
                spacecraft.Id,
                spacecraft.Name,
                spacecraft.Type,
                spacecraft.Capacity,
                spacecraft.Status))
            .ToListAsync();
    }

    private static Spacecraft ToDomain(SpacecraftEntity spacecraft)
    {
        return new Spacecraft(
            spacecraft.Id,
            spacecraft.Name,
            spacecraft.Type,
            spacecraft.Capacity,
            spacecraft.Status);
    }
}
