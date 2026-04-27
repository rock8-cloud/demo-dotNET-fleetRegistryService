namespace FleetRegistryService.Domain;

public sealed record Spacecraft(
    int Id,
    string Name,
    string Type,
    int Capacity,
    string Status);
