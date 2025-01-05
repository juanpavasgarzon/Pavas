using Application.Abstractions.Data;
using Domain.Customers;
using Domain.MeasurementUnits;
using Domain.Movements;
using Domain.Outbox;
using Domain.Products;
using Domain.Suppliers;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Database;

internal sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
) : DbContext(options), IApplicationDbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<TodoItem> TodoItems { get; init; }
    public DbSet<MeasurementUnit> MeasurementUnits { get; init; }
    public DbSet<Supplier> Suppliers { get; init; }
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<Movement> Movements { get; init; }
    public DbSet<MovementProduct> MovementProducts { get; init; }

    public DatabaseFacade GetDatabase() => Database;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
}
