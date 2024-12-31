using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Suppliers.Get;

internal sealed class GetSuppliersQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetSuppliersQuery, List<SupplierResponse>>
{
    public async Task<Result<List<SupplierResponse>>> Handle(GetSuppliersQuery request,
        CancellationToken cancellationToken)
    {
        List<SupplierResponse> suppliers = await context.Suppliers
            .Select(s => new SupplierResponse
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                Notes = s.Notes,
                CreatedAt = s.CreatedAt,
            })
            .ToListAsync(cancellationToken);

        return suppliers;
    }
}
