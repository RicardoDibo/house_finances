namespace HouseFinances.Application.Totals;

public interface ITotalsService
{
    Task<PersonTotalsSummaryDto> GetPersonTotalsAsync();
    Task<CategoryTotalsSummaryDto> GetCategoryTotalsAsync();
}
