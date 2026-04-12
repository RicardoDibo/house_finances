namespace HouseFinances.Application.Transactions;

public interface ITransactionService
{
    Task<IReadOnlyList<TransactionDto>> GetAllAsync();
    Task<TransactionDto> CreateAsync(CreateTransactionCommand command);
}
