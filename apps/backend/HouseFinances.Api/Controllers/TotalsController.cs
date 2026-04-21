using System.Security.Claims;
using HouseFinances.Application.Totals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseFinances.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TotalsController : ControllerBase
{
    private readonly ITotalsService _service;

    public TotalsController(ITotalsService service) => _service = service;

    [HttpGet("persons")]
    public async Task<IActionResult> GetPersonTotals()
    {
        var userId = IsAdmin() ? (Guid?)null : GetCurrentUserId();
        return Ok(await _service.GetPersonTotalsAsync(userId));
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoryTotals()
    {
        var userId = IsAdmin() ? (Guid?)null : GetCurrentUserId();
        return Ok(await _service.GetCategoryTotalsAsync(userId));
    }

    private Guid? GetCurrentUserId()
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return value is not null ? Guid.Parse(value) : null;
    }

    private bool IsAdmin() => User.IsInRole("Admin");
}
