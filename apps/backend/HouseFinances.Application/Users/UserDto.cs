using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Enums;

namespace HouseFinances.Application.Users;

public record UserDto(Guid Id, string Name, string Email, UserRole Role, string RoleName);

public record CreateUserCommand(string Name, string Email, string Password, UserRole Role);

public static class UserDtoExtensions
{
    public static UserDto ToDto(this User user) =>
        new(user.Id, user.Name, user.Email, user.Role, user.Role.ToString());
}
