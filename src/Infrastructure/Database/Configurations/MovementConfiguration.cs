using Domain.Movements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class MovementConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.Reference);

        builder.HasMany<MovementProduct>().WithOne().HasForeignKey(mp => mp.MovementId).OnDelete(DeleteBehavior.Cascade);
    }
}
