using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Movements;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Movements.GetById;

public sealed class GetMovementByIdQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetMovementByIdQuery, MovementResponse>
{
    public async Task<Result<MovementResponse>> Handle(GetMovementByIdQuery query, CancellationToken cancellationToken)
    {
        MovementResponse? movement = await context.Movements
            .AsNoTracking()
            .Where(p => p.Id == query.MovementId)
            .Include(m => m.MovementProducts)
            .ThenInclude(mp => mp.Product)
            .Select(movement => new MovementResponse
            {
                Id = movement.Id,
                Reference = movement.Reference,
                Type = movement.Type.ToString(),
                Notes = movement.Notes,
                CreatedAt = movement.CreatedAt,
                IsCompleted = movement.IsCompleted,
                CompletedAt = movement.CompletedAt,
                Products = movement.MovementProducts.Select(mp => new ProductResponse
                {
                    ProductId = mp.ProductId,
                    Code = mp.Product.Code,
                    Name = mp.Product.Name,
                    Quantity = mp.Quantity,
                    Notes = mp.Notes
                }).ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);

        return movement ?? Result.Failure<MovementResponse>(MovementErrors.NotFound);
    }
}
