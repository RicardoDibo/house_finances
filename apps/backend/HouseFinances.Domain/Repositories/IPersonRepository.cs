using HouseFinances.Domain.Entities;

namespace HouseFinances.Domain.Repositories;

public interface IPersonRepository
{
    Task<IReadOnlyList<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(Guid id);
    void Add(Person person);
    void Remove(Person person);
    Task SaveChangesAsync();
}
