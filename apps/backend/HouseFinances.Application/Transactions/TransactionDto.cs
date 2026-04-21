namespace HouseFinances.Application.Transactions;

public record TransactionDto(
    Guid Id,
    string Description,
    decimal Amount,
    int Type,
    string TypeName,
    Guid CategoryId,
    string CategoryDescription,
    Guid PersonId,
    string PersonName,
    Guid? UserId);

public record CreateTransactionCommand(
    string Description,
    decimal Amount,
    int Type,
    Guid CategoryId,
    Guid PersonId,
    Guid? UserId = null);
