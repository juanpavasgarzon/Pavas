using Domain.MeasurementUnits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.Symbol)
            .IsUnique();

        builder.HasMany(m => m.Products)
            .WithOne(p => p.MeasurementUnit)
            .HasForeignKey(p => p.MeasurementUnitId);
    }
}
