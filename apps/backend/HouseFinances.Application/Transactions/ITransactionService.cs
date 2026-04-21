namespace HouseFinances.Application.Transactions;

public interface ITransactionService
{
    Task<IReadOnlyList<TransactionDto>> GetAllAsync(Guid? userId = null);
    Task<TransactionDto> CreateAsync(CreateTransactionCommand command);
}
