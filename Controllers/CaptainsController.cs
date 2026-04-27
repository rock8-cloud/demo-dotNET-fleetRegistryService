using FleetRegistryService.Domain;
using FleetRegistryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetRegistryService.Controllers;

[ApiController]
[Route("captains")]
public sealed class CaptainsController(ICaptainService captainService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Captain>>> GetCaptains()
    {
        return Ok(await captainService.GetCaptainsAsync());
    }
}
