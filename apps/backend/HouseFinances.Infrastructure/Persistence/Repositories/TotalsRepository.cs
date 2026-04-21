using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HouseFinances.Infrastructure.Persistence.Repositories;

public class TotalsRepository : ITotalsRepository
{
    private readonly AppDbContext _context;

    public TotalsRepository(AppDbContext context) => _context = context;

    public async Task<IReadOnlyList<Person>> GetPersonsWithTransactionsAsync(Guid? userId = null)
    {
        if (userId.HasValue)
            return await _context.Persons
                .Include(p => p.Transactions.Where(t => t.UserId == userId.Value))
                .ToListAsync();

        return await _context.Persons.Include(p => p.Transactions).ToListAsync();
    }

    public async Task<IReadOnlyList<Category>> GetCategoriesWithTransactionsAsync(Guid? userId = null)
    {
        if (userId.HasValue)
            return await _context.Categories
                .Include(c => c.Transactions.Where(t => t.UserId == userId.Value))
                .ToListAsync();

        return await _context.Categories.Include(c => c.Transactions).ToListAsync();
    }
}
