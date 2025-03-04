﻿using Domain.Customers;
using Domain.MeasurementUnits;
using Domain.Movements;
using Domain.Outbox;
using Domain.Products;
using Domain.Suppliers;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext : IDisposable, IAsyncDisposable
{
    DbSet<OutboxMessage> OutboxMessages { get; init; }
    DbSet<User> Users { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<MeasurementUnit> MeasurementUnits { get; }
    DbSet<Product> Products { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Movement> Movements { get; init; }
    DbSet<MovementProduct> MovementProducts { get; init; }

    DatabaseFacade GetDatabase();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
