using HouseFinances.Domain.Entities;

namespace HouseFinances.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<User>> GetAllAsync();
    Task<bool> AnyAsync();
    void Add(User user);
    Task SaveChangesAsync();
}
