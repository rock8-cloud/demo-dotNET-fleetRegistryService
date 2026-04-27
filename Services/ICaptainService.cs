using FleetRegistryService.Domain;

namespace FleetRegistryService.Services;

public interface ICaptainService
{
    Task<IReadOnlyList<Captain>> GetCaptainsAsync();
}
