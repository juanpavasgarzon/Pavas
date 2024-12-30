using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Customers.Create;

internal class CreateCustomerCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<CreateCustomerCommand>
{
    public async Task<Result> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        if (await context.Customers.AnyAsync(c => c.Id == command.Id, cancellationToken))
        {
            return Result.Failure(CustomerErrors.IdNotUnique);
        }

        if (await context.Customers.AnyAsync(c => c.Email == command.Email, cancellationToken))
        {
            return Result.Failure(CustomerErrors.EmailNotUnique);
        }

        var customer = new Customer
        {
            Id = command.Id,
            Name = command.Name,
            Email = command.Email,
            Phone = command.Phone,
            Address = command.Address,
            Notes = command.Notes,
            CreatedAt = dateTimeProvider.UtcNow
        };

        context.Customers.Add(customer);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
