namespace FleetRegistryService.Repositories;

public interface IHealthRepository
{
    Task<bool> CanConnectAsync();
}
