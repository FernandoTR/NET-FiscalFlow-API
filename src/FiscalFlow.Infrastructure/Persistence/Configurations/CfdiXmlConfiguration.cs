using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class CfdiXmlConfiguration : IEntityTypeConfiguration<CfdiXml>
{
    public void Configure(EntityTypeBuilder<CfdiXml> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__CfdiXml__D14A66895961B32B");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.Cfdi)
               .WithMany(c => c.CfdiXmls)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__CfdiXml__CfdiId__534D60F1");
    }
}

