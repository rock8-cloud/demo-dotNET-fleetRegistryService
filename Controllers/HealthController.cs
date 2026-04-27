using FleetRegistryService.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetRegistryService.Controllers;

[ApiController]
[Route("health")]
public sealed class HealthController(IHealthService healthService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHealth()
    {
        var canConnect = await healthService.IsHealthyAsync();

        if (!canConnect)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "Unhealthy",
                service = "Fleet Registry Service"
            });
        }

        return Ok(new
        {
            status = "Healthy",
            service = "Fleet Registry Service"
        });
    }
}
