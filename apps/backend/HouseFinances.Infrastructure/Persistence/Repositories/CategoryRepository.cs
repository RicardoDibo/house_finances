using HouseFinances.Domain.Entities;
using HouseFinances.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HouseFinances.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) => _context = context;

    public async Task<IReadOnlyList<Category>> GetAllAsync() =>
        await _context.Categories.ToListAsync();

    public async Task<Category?> GetByIdAsync(Guid id) =>
        await _context.Categories.FindAsync(id);

    public void Add(Category category) => _context.Categories.Add(category);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}
