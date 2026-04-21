namespace HouseFinances.Application.Auth;

public interface IAuthService
{
    Task<AuthResult?> LoginAsync(LoginCommand command);
}
