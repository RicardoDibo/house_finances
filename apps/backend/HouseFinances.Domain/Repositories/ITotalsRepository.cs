using HouseFinances.Domain.Entities;

namespace HouseFinances.Domain.Repositories;

public interface ITotalsRepository
{
    Task<IReadOnlyList<User>> GetUsersWithTransactionsAsync(Guid? userId = null);
    Task<IReadOnlyList<Category>> GetCategoriesWithTransactionsAsync(Guid? userId = null);
}
