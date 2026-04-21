using HouseFinances.Domain.Entities;

namespace HouseFinances.Domain.Repositories;

public interface ITransactionRepository
{
    Task<IReadOnlyList<Transaction>> GetAllWithDetailsAsync(Guid? userId = null);
    void Add(Transaction transaction);
    Task SaveChangesAsync();
}
