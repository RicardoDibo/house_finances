using HouseFinances.Application.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace HouseFinances.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand command) =>
        Ok(await _service.CreateAsync(command));
}
