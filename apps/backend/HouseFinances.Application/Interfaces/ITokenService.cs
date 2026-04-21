using HouseFinances.Application.Users;

namespace HouseFinances.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(UserDto user);
}
