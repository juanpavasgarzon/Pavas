using Domain.MeasurementUnits;
using Domain.Products;
using Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Code).IsUnique();

        builder.HasOne<Supplier>().WithMany().HasForeignKey(p => p.SupplierId);

        builder.HasOne<MeasurementUnit>().WithMany().HasForeignKey(p => p.MeasurementUnitId);

        builder.Property(p => p.Price).HasPrecision(18, 6);

        builder.Property(p => p.StockQuantity).HasPrecision(18, 6);
    }
}
