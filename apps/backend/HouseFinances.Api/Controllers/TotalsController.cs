using HouseFinances.Application.Totals;
using Microsoft.AspNetCore.Mvc;

namespace HouseFinances.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TotalsController : ControllerBase
{
    private readonly ITotalsService _service;

    public TotalsController(ITotalsService service) => _service = service;

    [HttpGet("persons")]
    public async Task<IActionResult> GetPersonTotals() => Ok(await _service.GetPersonTotalsAsync());

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoryTotals() => Ok(await _service.GetCategoryTotalsAsync());
}
