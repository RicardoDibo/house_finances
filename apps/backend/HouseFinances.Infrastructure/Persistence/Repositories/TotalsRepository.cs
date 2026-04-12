using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HouseFinances.Infrastructure.Persistence.Repositories;

public class TotalsRepository : ITotalsRepository
{
    private readonly AppDbContext _context;

    public TotalsRepository(AppDbContext context) => _context = context;

    public async Task<IReadOnlyList<Person>> GetPersonsWithTransactionsAsync() =>
        await _context.Persons.Include(p => p.Transactions).ToListAsync();

    public async Task<IReadOnlyList<Category>> GetCategoriesWithTransactionsAsync() =>
        await _context.Categories.Include(c => c.Transactions).ToListAsync();
}
