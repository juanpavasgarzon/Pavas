using Domain.Movements;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class MovementProductConfiguration : IEntityTypeConfiguration<MovementProduct>
{
    public void Configure(EntityTypeBuilder<MovementProduct> builder)
    {
        builder.HasKey(mp => new { mp.MovementId, mp.ProductId });

        builder.HasOne<Product>().WithMany().HasForeignKey(mp => mp.ProductId);

        builder.HasOne<Movement>().WithMany().HasForeignKey(mp => mp.MovementId).OnDelete(DeleteBehavior.Cascade);
    }
}
