using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Exceptions;
using HouseFinances.Domain.Repositories;

namespace HouseFinances.Application.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactions;
    private readonly ICategoryRepository _categories;
    private readonly IUserRepository _users;

    public TransactionService(
        ITransactionRepository transactions,
        ICategoryRepository categories,
        IUserRepository users)
    {
        _transactions = transactions;
        _categories = categories;
        _users = users;
    }

    public async Task<IReadOnlyList<TransactionDto>> GetAllAsync(Guid? userId = null)
    {
        var list = await _transactions.GetAllWithDetailsAsync(userId);
        return list.Select(ToDto).ToList();
    }

    public async Task<TransactionDto> CreateAsync(CreateTransactionCommand command)
    {
        if (!Enum.IsDefined(typeof(TransactionType), command.Type))
            throw new DomainException("Invalid transaction type. Use 0 (Expense) or 1 (Income).");

        var user = await _users.GetByIdAsync(command.UserId)
            ?? throw new KeyNotFoundException("User not found.");
        var category = await _categories.GetByIdAsync(command.CategoryId)
            ?? throw new KeyNotFoundException("Category not found.");

        var type = (TransactionType)command.Type;
        var transaction = Transaction.Create(command.Description, command.Amount, type, user, category);

        _transactions.Add(transaction);
        await _transactions.SaveChangesAsync();
        return ToDto(transaction);
    }

    private static TransactionDto ToDto(Transaction t) => new(
        t.Id, t.Description, t.Amount,
        (int)t.Type, t.Type.ToString(),
        t.CategoryId, t.Category.Description,
        t.UserId, t.User.Name);
}
