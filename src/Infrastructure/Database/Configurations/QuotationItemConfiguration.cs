using Domain.Quotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class QuotationItemConfiguration //: IEntityTypeConfiguration<QuotationItem>
{
    public void Configure(EntityTypeBuilder<QuotationItem> builder)
    {
        builder.HasKey(q => q.Id);

       // builder.HasOne<Quotation>().WithMany().HasForeignKey(q => q.QuotationId);
    }
}
