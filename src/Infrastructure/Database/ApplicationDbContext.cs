using Application.Abstractions.Data;
using Domain.Customers;
using Domain.Invoices;
using Domain.Orders;
using Domain.Products;
using Domain.Purchases;
using Domain.Quotations;
using Domain.Suppliers;
using Domain.Todos;
using Domain.Users;
using Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
) : DbContext(options), IApplicationDbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<TodoItem> TodoItems { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<Supplier> Suppliers { get; init; }
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Order> Orders { get; init; }
    public DbSet<Purchase> Purchases { get; init; }
    public DbSet<Quotation> Quotations { get; init; }
    public DbSet<Invoice> Invoices { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }
}
