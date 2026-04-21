using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HouseFinances.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;

    public async Task<User?> GetByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant());

    public async Task<User?> GetByIdAsync(Guid id) =>
        await _context.Users.FindAsync(id);

    public async Task<IReadOnlyList<User>> GetAllAsync() =>
        await _context.Users.ToListAsync();

    public async Task<bool> AnyAsync() =>
        await _context.Users.AnyAsync();

    public void Add(User user) => _context.Users.Add(user);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
