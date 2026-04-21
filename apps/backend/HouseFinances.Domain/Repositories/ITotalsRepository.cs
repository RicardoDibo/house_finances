using HouseFinances.Domain.Entities;

namespace HouseFinances.Domain.Repositories;

public interface ITotalsRepository
{
    Task<IReadOnlyList<Person>> GetPersonsWithTransactionsAsync(Guid? userId = null);
    Task<IReadOnlyList<Category>> GetCategoriesWithTransactionsAsync(Guid? userId = null);
}
