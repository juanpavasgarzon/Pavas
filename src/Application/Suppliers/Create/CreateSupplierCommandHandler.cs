using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Suppliers.Create;

internal sealed class CreateSupplierCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<CreateSupplierCommand>
{
    public async Task<Result> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
    {
        if (await context.Suppliers.AnyAsync(c => c.Id == command.Id, cancellationToken))
        {
            return Result.Failure(SupplierErrors.IdNotUnique);
        }

        if (await context.Suppliers.AnyAsync(c => c.Email == command.Email, cancellationToken))
        {
            return Result.Failure(SupplierErrors.EmailNotUnique);
        }

        var supplier = new Supplier
        {
            Id = command.Id,
            Name = command.Name,
            Email = command.Email,
            Phone = command.Phone,
            Address = command.Address,
            Notes = command.Notes,
            CreatedAt = dateTimeProvider.UtcNow
        };

        context.Suppliers.Add(supplier);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
