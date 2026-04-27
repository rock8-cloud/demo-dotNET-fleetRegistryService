using FleetRegistryService.Domain;
using FleetRegistryService.Models;
using FleetRegistryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetRegistryService.Controllers;

[ApiController]
[Route("fleet")]
public sealed class FleetController(IFleetService fleetService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Spacecraft>>> GetFleet()
    {
        return Ok(await fleetService.GetFleetAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Spacecraft>> GetSpacecraft(int id)
    {
        var spacecraft = await fleetService.GetSpacecraftAsync(id);

        if (spacecraft is null)
        {
            return NotFound(new { message = $"Spacecraft {id} was not found." });
        }

        return Ok(spacecraft);
    }

    [HttpPost]
    public async Task<ActionResult<Spacecraft>> CreateSpacecraft(CreateSpacecraftRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.Type) ||
            string.IsNullOrWhiteSpace(request.Status))
        {
            return BadRequest(new { message = "Name, type and status are required." });
        }

        var command = new CreateSpacecraftCommand(
            request.Name!,
            request.Type!,
            request.Capacity,
            request.Status!);

        var spacecraft = await fleetService.CreateSpacecraftAsync(command);

        return CreatedAtAction(nameof(GetSpacecraft), new { id = spacecraft.Id }, spacecraft);
    }

    [HttpGet("available")]
    public async Task<ActionResult<IReadOnlyList<Spacecraft>>> GetAvailableSpacecraft()
    {
        return Ok(await fleetService.GetAvailableSpacecraftAsync());
    }
}
