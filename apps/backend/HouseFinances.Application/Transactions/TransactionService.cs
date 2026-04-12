using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Exceptions;
using HouseFinances.Domain.Repositories;

namespace HouseFinances.Application.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactions;
    private readonly IPersonRepository _persons;
    private readonly ICategoryRepository _categories;

    public TransactionService(
        ITransactionRepository transactions,
        IPersonRepository persons,
        ICategoryRepository categories)
    {
        _transactions = transactions;
        _persons = persons;
        _categories = categories;
    }

    public async Task<IReadOnlyList<TransactionDto>> GetAllAsync()
    {
        var list = await _transactions.GetAllWithDetailsAsync();
        return list.Select(ToDto).ToList();
    }

    public async Task<TransactionDto> CreateAsync(CreateTransactionCommand command)
    {
        if (!Enum.IsDefined(typeof(TransactionType), command.Type))
            throw new DomainException("Invalid transaction type. Use 0 (Expense) or 1 (Income).");

        var person = await _persons.GetByIdAsync(command.PersonId)
            ?? throw new KeyNotFoundException("Person not found.");
        var category = await _categories.GetByIdAsync(command.CategoryId)
            ?? throw new KeyNotFoundException("Category not found.");

        var type = (TransactionType)command.Type;
        var transaction = Transaction.Create(command.Description, command.Amount, type, person, category);

        _transactions.Add(transaction);
        await _transactions.SaveChangesAsync();
        return ToDto(transaction);
    }

    private static TransactionDto ToDto(Transaction t) => new(
        t.Id, t.Description, t.Amount,
        (int)t.Type, t.Type.ToString(),
        t.CategoryId, t.Category.Description,
        t.PersonId, t.Person.Name);
}
