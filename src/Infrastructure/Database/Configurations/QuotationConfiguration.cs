using Domain.Quotations;
using Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class QuotationConfiguration //: IEntityTypeConfiguration<Quotation>
{
    public void Configure(EntityTypeBuilder<Quotation> builder)
    {
        builder.HasKey(q => q.Id);

        builder.HasOne<Supplier>().WithMany().HasForeignKey(q => q.SupplierId);

        builder.Property(q => q.TotalAmount).HasDefaultValue(decimal.Zero).IsRequired();

        // builder.HasMany<QuotationItem>().WithOne().HasForeignKey(i => i.QuotationId);
    }
}
