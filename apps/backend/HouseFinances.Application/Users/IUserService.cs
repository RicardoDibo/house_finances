namespace HouseFinances.Application.Users;

public interface IUserService
{
    Task<IReadOnlyList<UserDto>> GetAllAsync();
    Task<UserDto> CreateAsync(CreateUserCommand command);
}
