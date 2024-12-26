using Domain.Invoices;
using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Invoices;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasOne<Quotation>().WithMany().HasForeignKey(i => i.QuotationId);

        builder.Property(i => i.TotalAmount).HasDefaultValue(decimal.Zero).IsRequired();
    }
}
