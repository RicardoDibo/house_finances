using HouseFinances.Domain.Entities;

namespace HouseFinances.Domain.Repositories;

public interface ITransactionRepository
{
    Task<IReadOnlyList<Transaction>> GetAllWithDetailsAsync();
    void Add(Transaction transaction);
    Task SaveChangesAsync();
}
