using HouseFinances.Domain.Enums;
using HouseFinances.Domain.Exceptions;

namespace HouseFinances.Domain.Entities;

public class Category
{
    public Guid Id { get; protected set; }
    public string Description { get; protected set; } = string.Empty;
    public CategoryPurpose Purpose { get; protected set; }
    public ICollection<Transaction> Transactions { get; protected set; } = new List<Transaction>();

    protected Category() { }

    public static Category Create(string description, CategoryPurpose purpose)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Description is required.");
        if (description.Length > 400)
            throw new DomainException("Description must be at most 400 characters.");

        return new Category
        {
            Id = Guid.NewGuid(),
            Description = description.Trim(),
            Purpose = purpose
        };
    }

    // Domain rule: checks if this category is compatible with a transaction type
    public bool IsCompatibleWith(TransactionType type) =>
        Purpose == CategoryPurpose.Both
        || (type == TransactionType.Expense && Purpose == CategoryPurpose.Expense)
        || (type == TransactionType.Income && Purpose == CategoryPurpose.Income);
}
