namespace HouseFinances.Application.Auth;

public record LoginCommand(string Email, string Password);

public record AuthResult(string Token, Users.UserDto User);
