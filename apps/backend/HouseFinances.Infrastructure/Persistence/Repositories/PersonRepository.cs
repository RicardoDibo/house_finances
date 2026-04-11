using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HouseFinances.Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context) => _context = context;

    public async Task<IReadOnlyList<Person>> GetAllAsync() =>
        await _context.Persons.ToListAsync();

    public async Task<Person?> GetByIdAsync(Guid id) =>
        await _context.Persons.FindAsync(id);

    public void Add(Person person) => _context.Persons.Add(person);

    public void Remove(Person person) => _context.Persons.Remove(person);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
