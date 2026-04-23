using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HouseFinances.Infrastructure.Persistence.Repositories;

public class TotalsRepository : ITotalsRepository
{
    private readonly AppDbContext _context;

    public TotalsRepository(AppDbContext context) => _context = context;

    public async Task<IReadOnlyList<User>> GetUsersWithTransactionsAsync(Guid? userId = null)
    {
        if (userId.HasValue)
            return await _context.Users
                .Include(u => u.Transactions.Where(t => t.UserId == userId.Value))
                    .ThenInclude(t => t.Category)
                .Where(u => u.Id == userId.Value)
                .ToListAsync();

        return await _context.Users
            .Include(u => u.Transactions)
                .ThenInclude(t => t.Category)
            .ToListAsync();
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
