using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HouseFinances.Infrastructure.Persistence;

// Used by `dotnet ef` CLI when running migrations from this project
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("Host=localhost;Database=house_finances;Username=hfuser;Password=hfpassword")
            .Options;
        return new AppDbContext(options);
    }
}
