using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Movements.Get;

internal sealed class GetMovementsQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetMovementsQuery, List<MovementResponse>>
{
    public async Task<Result<List<MovementResponse>>> Handle(GetMovementsQuery request,
        CancellationToken cancellationToken)
    {
        List<MovementResponse> movements = await context.Movements
            .AsNoTracking()
            .Select(movement => new MovementResponse
            {
                Id = movement.Id,
                Reference = movement.Reference,
                Type = movement.Type.ToString(),
                Notes = movement.Notes,
                CreatedAt = movement.CreatedAt,
                IsCompleted = movement.IsCompleted,
                CompletedAt = movement.CompletedAt
            })
            .ToListAsync(cancellationToken);

        return movements;
    }
}
