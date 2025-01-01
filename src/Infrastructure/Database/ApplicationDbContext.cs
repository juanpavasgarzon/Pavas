using Application.Abstractions.Data;
using Domain.Customers;
using Domain.MeasurementUnits;
using Domain.Outbox;
using Domain.Products;
using Domain.Quotations;
using Domain.Suppliers;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

internal sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
) : DbContext(options), IApplicationDbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<TodoItem> TodoItems { get; init; }
    public DbSet<MeasurementUnit> MeasurementUnits { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<Supplier> Suppliers { get; init; }
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Quotation> Quotations { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
}
