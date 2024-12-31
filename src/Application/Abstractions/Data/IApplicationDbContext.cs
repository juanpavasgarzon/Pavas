using Domain.Customers;
using Domain.Invoices;
using Domain.Orders;
using Domain.Outbox;
using Domain.Products;
using Domain.Purchases;
using Domain.Quotations;
using Domain.Suppliers;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<OutboxMessage> OutboxMessages { get; init; }
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Product> Products { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Order> Orders { get; }
    DbSet<Purchase> Purchases { get; }
    DbSet<Quotation> Quotations { get; }
    DbSet<Invoice> Invoices { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
