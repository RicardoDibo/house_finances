using HouseFinances.Domain.Entities;

namespace HouseFinances.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    void Add(Category category);
    Task SaveChangesAsync();
}
