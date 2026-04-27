using FleetRegistryService.Domain;

namespace FleetRegistryService.Repositories;

public interface IFleetRepository
{
    Task<IReadOnlyList<Spacecraft>> GetAllAsync();

    Task<Spacecraft?> GetByIdAsync(int id);

    Task<Spacecraft> AddAsync(Spacecraft spacecraft);

    Task<IReadOnlyList<Spacecraft>> GetByStatusAsync(string status);
}
