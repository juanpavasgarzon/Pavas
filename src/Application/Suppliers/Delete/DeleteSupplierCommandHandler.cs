using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Suppliers.Delete;

internal sealed class DeleteSupplierCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteSupplierCommand>
{
    public async Task<Result> Handle(DeleteSupplierCommand command, CancellationToken cancellationToken)
    {
        Supplier? supplier = await context.Suppliers
            .SingleOrDefaultAsync(s => s.Id == command.SupplierId, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure(SupplierErrors.NotFound);
        }

        try
        {
            context.Suppliers.Remove(supplier);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(SupplierErrors.CanNotDelete);
        }
    }
}
