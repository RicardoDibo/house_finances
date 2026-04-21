using HouseFinances.Application.Interfaces;
using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Exceptions;
using HouseFinances.Domain.Repositories;

namespace HouseFinances.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository users, IPasswordHasher passwordHasher)
    {
        _users = users;
        _passwordHasher = passwordHasher;
    }

    public async Task<IReadOnlyList<UserDto>> GetAllAsync()
    {
        var list = await _users.GetAllAsync();
        return list.Select(u => u.ToDto()).ToList();
    }

    public async Task<UserDto> CreateAsync(CreateUserCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
            throw new DomainException("Email is required.");
        if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length < 6)
            throw new DomainException("Password must be at least 6 characters.");

        var existing = await _users.GetByEmailAsync(command.Email);
        if (existing is not null)
            throw new DomainException("A user with this email already exists.");

        var hash = _passwordHasher.Hash(command.Password);
        var user = User.Create(command.Name, command.Email, hash, command.Role);

        _users.Add(user);
        await _users.SaveChangesAsync();
        return user.ToDto();
    }
}
