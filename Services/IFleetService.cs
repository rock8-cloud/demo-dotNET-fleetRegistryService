using FleetRegistryService.Domain;

namespace FleetRegistryService.Services;

public interface IFleetService
{
    Task<IReadOnlyList<Spacecraft>> GetFleetAsync();

    Task<Spacecraft?> GetSpacecraftAsync(int id);

    Task<Spacecraft> CreateSpacecraftAsync(CreateSpacecraftCommand command);

    Task<IReadOnlyList<Spacecraft>> GetAvailableSpacecraftAsync();
}
