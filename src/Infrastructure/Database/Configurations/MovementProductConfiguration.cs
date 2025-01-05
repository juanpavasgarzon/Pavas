using Domain.Movements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class MovementProductConfiguration : IEntityTypeConfiguration<MovementProduct>
{
    public void Configure(EntityTypeBuilder<MovementProduct> builder)
    {
        builder.HasKey(mp => new { mp.MovementId, mp.ProductId });

        builder.HasOne(mp => mp.Product)
            .WithMany(p => p.MovementProducts)
            .HasForeignKey(mp => mp.ProductId);

        builder.HasOne(mp => mp.Movement)
            .WithMany(m => m.MovementProducts)
            .HasForeignKey(mp => mp.MovementId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
