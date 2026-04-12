using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Repositories;

namespace HouseFinances.Application.Totals;

public class TotalsService : ITotalsService
{
    private readonly ITotalsRepository _repository;

    public TotalsService(ITotalsRepository repository) => _repository = repository;

    public async Task<PersonTotalsSummaryDto> GetPersonTotalsAsync()
    {
        var persons = await _repository.GetPersonsWithTransactionsAsync();

        var rows = persons.Select(p => new PersonTotalsDto(
            p.Id,
            p.Name,
            TotalIncome: p.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
            TotalExpenses: p.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount),
            Balance: p.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount)
                   - p.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
        )).ToList();

        return new PersonTotalsSummaryDto(
            rows,
            rows.Sum(r => r.TotalIncome),
            rows.Sum(r => r.TotalExpenses),
            rows.Sum(r => r.Balance));
    }

    public async Task<CategoryTotalsSummaryDto> GetCategoryTotalsAsync()
    {
        var categories = await _repository.GetCategoriesWithTransactionsAsync();

        var rows = categories.Select(c => new CategoryTotalsDto(
            c.Id,
            c.Description,
            TotalIncome: c.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
            TotalExpenses: c.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount),
            Balance: c.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount)
                   - c.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
        )).ToList();

        return new CategoryTotalsSummaryDto(
            rows,
            rows.Sum(r => r.TotalIncome),
            rows.Sum(r => r.TotalExpenses),
            rows.Sum(r => r.Balance));
    }
}
