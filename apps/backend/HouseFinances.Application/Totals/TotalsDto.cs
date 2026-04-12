namespace HouseFinances.Application.Totals;

public record PersonTotalsDto(
    Guid Id,
    string Name,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal Balance);

public record PersonTotalsSummaryDto(
    IReadOnlyList<PersonTotalsDto> Persons,
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
