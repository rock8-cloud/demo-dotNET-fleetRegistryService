using FleetRegistryService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetRegistryService.Data;

public sealed class FleetRegistryDbContext(DbContextOptions<FleetRegistryDbContext> options) : DbContext(options)
{
    public DbSet<SpacecraftEntity> Spacecraft => Set<SpacecraftEntity>();

    public DbSet<CaptainEntity> Captains => Set<CaptainEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("fleet_registry");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetRegistryDbContext).Assembly);
    }
}
