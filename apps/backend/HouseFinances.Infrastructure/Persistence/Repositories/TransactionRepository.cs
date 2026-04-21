using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HouseFinances.Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context) => _context = context;

    public async Task<IReadOnlyList<Transaction>> GetAllWithDetailsAsync(Guid? userId = null)
    {
        var query = _context.Transactions
            .Include(t => t.Category)
            .Include(t => t.Person)
            .AsQueryable();

        if (userId.HasValue)
            query = query.Where(t => t.UserId == userId.Value);

        return await query.ToListAsync();
    }

    public void Add(Transaction transaction) => _context.Transactions.Add(transaction);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
