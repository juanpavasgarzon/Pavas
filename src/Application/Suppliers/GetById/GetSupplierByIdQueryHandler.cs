using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Suppliers.GetById;

public sealed class GetSupplierByIdQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetSupplierByIdQuery, SupplierResponse>
{
    public async Task<Result<SupplierResponse>> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
    {
        SupplierResponse? supplier = await context.Suppliers
            .AsNoTracking()
            .Where(s => s.Id == query.SupplierId)
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
            .SingleOrDefaultAsync(cancellationToken);

        return supplier ?? Result.Failure<SupplierResponse>(SupplierErrors.NotFound);
    }
}
