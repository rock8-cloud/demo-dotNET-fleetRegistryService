using FleetRegistryService.Domain;

namespace FleetRegistryService.Repositories;

public interface ICaptainRepository
{
    Task<IReadOnlyList<Captain>> GetAllAsync();
}
