using HouseFinances.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HouseFinances.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _authService.LoginAsync(command);
        if (result is null)
            return Unauthorized(new { title = "Invalid email or password." });

        return Ok(result);
    }
}
