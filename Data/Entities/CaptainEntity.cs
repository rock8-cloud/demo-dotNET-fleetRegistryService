namespace FleetRegistryService.Data.Entities;

public sealed class CaptainEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Rank { get; set; } = string.Empty;
}
