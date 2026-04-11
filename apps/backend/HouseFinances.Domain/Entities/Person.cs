using HouseFinances.Domain.Exceptions;

namespace HouseFinances.Domain.Entities;

public class Person
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public int Age { get; protected set; }
    public ICollection<Transaction> Transactions { get; protected set; } = new List<Transaction>();

    protected Person() { }

    public static Person Create(string name, int age)
    {
        Validate(name, age);
        return new Person
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Age = age
        };
    }

    public void Update(string name, int age)
    {
        Validate(name, age);
        Name = name.Trim();
        Age = age;
    }

    public bool IsMinor => Age < 18;

    private static void Validate(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required.");
        if (name.Length > 200)
            throw new DomainException("Name must be at most 200 characters.");
        if (age < 0)
            throw new DomainException("Age must be non-negative.");
    }
}
