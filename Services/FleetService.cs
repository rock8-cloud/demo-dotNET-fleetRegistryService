using FleetRegistryService.Domain;
using FleetRegistryService.Repositories;

namespace FleetRegistryService.Services;

public sealed class FleetService(IFleetRepository fleetRepository) : IFleetService
{
    public async Task<IReadOnlyList<Spacecraft>> GetFleetAsync()
    {
        return await fleetRepository.GetAllAsync();
    }

    public async Task<Spacecraft?> GetSpacecraftAsync(int id)
    {
        return await fleetRepository.GetByIdAsync(id);
    }

    public async Task<Spacecraft> CreateSpacecraftAsync(CreateSpacecraftCommand command)
    {
        var spacecraft = new Spacecraft(
            Id: 0,
            Name: command.Name.Trim(),
            Type: command.Type.Trim(),
            Capacity: command.Capacity,
            Status: command.Status.Trim().ToUpperInvariant());

        return await fleetRepository.AddAsync(spacecraft);
    }

    public async Task<IReadOnlyList<Spacecraft>> GetAvailableSpacecraftAsync()
    {
        return await fleetRepository.GetByStatusAsync(SpacecraftStatuses.Active);
    }
}
