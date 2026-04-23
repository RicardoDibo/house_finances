namespace HouseFinances.Application.Totals;

public record UserTotalsDto(
    Guid Id,
    string Name,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal Balance);

public record UserTotalsSummaryDto(
    IReadOnlyList<UserTotalsDto> Users,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal Balance);

public record CategoryTotalsDto(
    Guid Id,
    string Description,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal Balance);

public record CategoryTotalsSummaryDto(
    IReadOnlyList<CategoryTotalsDto> Categories,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal Balance);
