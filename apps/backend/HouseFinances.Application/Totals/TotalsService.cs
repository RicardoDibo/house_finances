using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Repositories;

namespace HouseFinances.Application.Totals;

public class TotalsService : ITotalsService
{
    private readonly ITotalsRepository _repository;

    public TotalsService(ITotalsRepository repository) => _repository = repository;

    public async Task<UserTotalsSummaryDto> GetUserTotalsAsync(Guid? userId = null)
    {
        var users = await _repository.GetUsersWithTransactionsAsync(userId);

        var rows = users.Select(u => new UserTotalsDto(
            u.Id,
            u.Name,
            TotalIncome: u.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
            TotalExpenses: u.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount),
            Balance: u.Transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount)
                   - u.Transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
        )).ToList();

        return new UserTotalsSummaryDto(
            rows,
            rows.Sum(r => r.TotalIncome),
            rows.Sum(r => r.TotalExpenses),
            rows.Sum(r => r.Balance));
    }

    public async Task<CategoryTotalsSummaryDto> GetCategoryTotalsAsync(Guid? userId = null)
    {
        var categories = await _repository.GetCategoriesWithTransactionsAsync(userId);

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
