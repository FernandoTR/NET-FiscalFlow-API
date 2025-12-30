using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class BatchItemConfiguration : IEntityTypeConfiguration<BatchItem>
{
    public void Configure(EntityTypeBuilder<BatchItem> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__BatchIte__3214EC07B445316F");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.HasOne(x => x.Batch)
               .WithMany(b => b.BatchItems)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__BatchItem__Batch__6A30C649");

        builder.HasOne(x => x.Cfdi)
               .WithMany(c => c.BatchItems)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__BatchItem__CfdiI__6B24EA82");
    }
}

