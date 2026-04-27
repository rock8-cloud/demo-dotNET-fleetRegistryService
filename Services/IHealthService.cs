namespace FleetRegistryService.Services;

public interface IHealthService
{
    Task<bool> IsHealthyAsync();
}
