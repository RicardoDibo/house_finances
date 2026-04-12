using HouseFinances.Domain.Entities;

namespace HouseFinances.Domain.Repositories;

public interface ITotalsRepository
{
    Task<IReadOnlyList<Person>> GetPersonsWithTransactionsAsync();
    Task<IReadOnlyList<Category>> GetCategoriesWithTransactionsAsync();
}
