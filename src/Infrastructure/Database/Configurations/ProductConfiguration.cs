using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Code)
            .IsUnique();

        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId);

        builder.HasOne(p => p.MeasurementUnit)
            .WithMany(mu => mu.Products)
            .HasForeignKey(p => p.MeasurementUnitId);

        builder.Property(p => p.Price)
            .HasPrecision(18, 6);

        builder.Property(p => p.StockQuantity)
            .HasPrecision(18, 6);
    }
}
