namespace FleetRegistryService.Domain;

public sealed record CreateSpacecraftCommand(
    string Name,
    string Type,
    int Capacity,
    string Status);
