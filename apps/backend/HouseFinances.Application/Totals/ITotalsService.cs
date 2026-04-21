namespace HouseFinances.Application.Totals;

public interface ITotalsService
{
    Task<PersonTotalsSummaryDto> GetPersonTotalsAsync(Guid? userId = null);
    Task<CategoryTotalsSummaryDto> GetCategoryTotalsAsync(Guid? userId = null);
}
