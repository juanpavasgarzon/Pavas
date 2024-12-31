using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Customers.Get;

public class GetCustomersQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetCustomersQuery, List<CustomerResponse>>
{
    public async Task<Result<List<CustomerResponse>>> Handle(GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        List<CustomerResponse> customers = await context.Customers
            .Select(c => new CustomerResponse
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                Notes = c.Notes,
                CreatedAt = c.CreatedAt,
            })
            .ToListAsync(cancellationToken);

        return customers;
    }
}
