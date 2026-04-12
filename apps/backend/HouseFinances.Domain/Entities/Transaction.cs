using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Exceptions;

namespace HouseFinances.Domain.Entities;

public class Transaction
{
    public Guid Id { get; protected set; }
    public string Description { get; protected set; } = string.Empty;
    public decimal Amount { get; protected set; }
    public TransactionType Type { get; protected set; }
    public Guid CategoryId { get; protected set; }
    public Category Category { get; protected set; } = null!;
    public Guid PersonId { get; protected set; }
    public Person Person { get; protected set; } = null!;

    protected Transaction() { }

    public static Transaction Create(
        string description,
        decimal amount,
        TransactionType type,
        Person person,
        Category category)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description is required.");
        if (description.Length > 400)
            throw new DomainException("Description must be at most 400 characters.");
        if (amount <= 0)
            throw new DomainException("Amount must be a positive value.");

        // Domain rules enforced here
        if (person.IsMinor && type == TransactionType.Income)
            throw new DomainException("Minors (under 18) can only register expense transactions.");

        if (!category.IsCompatibleWith(type))
            throw new DomainException(
                $"Category purpose '{category.Purpose}' is incompatible with transaction type '{type}'.");

        return new Transaction
        {
            Id = Guid.NewGuid(),
            Description = description.Trim(),
            Amount = amount,
            Type = type,
            PersonId = person.Id,
            Person = person,
            CategoryId = category.Id,
            Category = category
        };
    }
}
