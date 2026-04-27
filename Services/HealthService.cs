using FleetRegistryService.Repositories;

namespace FleetRegistryService.Services;

public sealed class HealthService(IHealthRepository healthRepository) : IHealthService
{
    public async Task<bool> IsHealthyAsync()
    {
        return await healthRepository.CanConnectAsync();
    }
}
