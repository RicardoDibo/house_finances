using HouseFinances.Application.Persons;
using Microsoft.AspNetCore.Mvc;

namespace HouseFinances.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _service;

    public PersonsController(IPersonService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var person = await _service.GetByIdAsync(id);
        return person is null ? NotFound() : Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        var person = await _service.CreateAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonCommand command) =>
        Ok(await _service.UpdateAsync(id, command));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
