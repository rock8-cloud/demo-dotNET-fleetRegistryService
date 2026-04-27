using FleetRegistryService.Domain;
using FleetRegistryService.Repositories;

namespace FleetRegistryService.Services;

public sealed class CaptainService(ICaptainRepository captainRepository) : ICaptainService
{
    public async Task<IReadOnlyList<Captain>> GetCaptainsAsync()
    {
        return await captainRepository.GetAllAsync();
    }
}
