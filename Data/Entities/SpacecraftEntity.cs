namespace FleetRegistryService.Data.Entities;

public sealed class SpacecraftEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public string Status { get; set; } = string.Empty;
}
