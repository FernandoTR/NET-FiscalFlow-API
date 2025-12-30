using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class CfdiPdfConfiguration : IEntityTypeConfiguration<CfdiPdf>
{
    public void Configure(EntityTypeBuilder<CfdiPdf> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__CfdiPdf__3214EC077BD0039A");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.Cfdi)
               .WithMany(c => c.CfdiPdfs)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__CfdiPdf__CfdiId__5812160E");
    }
}

