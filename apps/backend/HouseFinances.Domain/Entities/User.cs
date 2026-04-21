using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Exceptions;

namespace HouseFinances.Domain.Entities;

public class User
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public string Email { get; protected set; } = string.Empty;
    public string PasswordHash { get; protected set; } = string.Empty;
    public UserRole Role { get; protected set; }
    public ICollection<Transaction> Transactions { get; protected set; } = [];

    protected User() { }

    public static User Create(string name, string email, string passwordHash, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required.");
        if (name.Length > 200)
            throw new DomainException("Name must be at most 200 characters.");
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required.");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Password hash is required.");

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            Role = role
        };
    }
}
