using System.Security.Claims;
using HouseFinances.Application.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseFinances.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = IsAdmin() ? (Guid?)null : GetCurrentUserId();
        return Ok(await _service.GetAllAsync(userId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command)
    {
        var userId = GetCurrentUserId();
        return Ok(await _service.CreateAsync(command with { UserId = userId }));
    }

    private Guid? GetCurrentUserId()
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return value is not null ? Guid.Parse(value) : null;
    }

    private bool IsAdmin() => User.IsInRole("Admin");
}
