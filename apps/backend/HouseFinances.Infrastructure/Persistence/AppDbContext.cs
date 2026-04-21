using HouseFinances.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HouseFinances.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(b =>
        {
            b.HasKey(p => p.Id);
            b.Property(p => p.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Category>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Description).IsRequired().HasMaxLength(400);
        });

        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(u => u.Id);
            b.Property(u => u.Name).IsRequired().HasMaxLength(200);
            b.Property(u => u.Email).IsRequired().HasMaxLength(256);
            b.HasIndex(u => u.Email).IsUnique();
        });

        modelBuilder.Entity<Transaction>(b =>
        {
            b.HasKey(t => t.Id);
            b.Property(t => t.Description).IsRequired().HasMaxLength(400);

            b.HasOne(t => t.Person)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
