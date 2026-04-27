using System.ComponentModel.DataAnnotations;

namespace FleetRegistryService.Models;

public sealed class CreateSpacecraftRequest
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Type { get; set; }

    [Range(0, int.MaxValue)]
    public int Capacity { get; set; }

    [Required]
    public string? Status { get; set; }
}
