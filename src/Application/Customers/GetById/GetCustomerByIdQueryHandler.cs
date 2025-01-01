using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Customers.GetById;

public class GetCustomerByIdQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetCustomerByIdQuery, CustomerResponse>
{
    public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        CustomerResponse? customer = await context.Customers
            .Where(c => c.Id == query.CustomerId)
            .Select(s => new CustomerResponse
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                Notes = s.Notes,
                CreatedAt = s.CreatedAt,
            })
            .SingleOrDefaultAsync(cancellationToken);

        return customer ?? Result.Failure<CustomerResponse>(CustomerErrors.NotFound);
    }
}
