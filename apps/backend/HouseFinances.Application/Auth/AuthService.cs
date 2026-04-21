using HouseFinances.Application.Interfaces;
using HouseFinances.Application.Users;
using HouseFinances.Domain.Repositories;

namespace HouseFinances.Application.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository users, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _users = users;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResult?> LoginAsync(LoginCommand command)
    {
        var user = await _users.GetByEmailAsync(command.Email);
        if (user is null) return null;
        if (!_passwordHasher.Verify(command.Password, user.PasswordHash)) return null;

        var dto = user.ToDto();
        var token = _tokenService.GenerateToken(dto);
        return new AuthResult(token, dto);
    }
}
