namespace HouseFinances.Application.Totals;

public interface ITotalsService
{
    Task<UserTotalsSummaryDto> GetUserTotalsAsync(Guid? userId = null);
    Task<CategoryTotalsSummaryDto> GetCategoryTotalsAsync(Guid? userId = null);
}
