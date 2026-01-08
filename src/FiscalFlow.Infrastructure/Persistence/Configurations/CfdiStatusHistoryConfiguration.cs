using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class CfdiStatusHistoryConfiguration : IEntityTypeConfiguration<CfdiStatusHistory>
{
    public void Configure(EntityTypeBuilder<CfdiStatusHistory> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__CfdiStat__3214EC07806CC8C9");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.ChangedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.Cfdi)
               .WithMany(c => c.CfdiStatusHistories)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__CfdiStatu__CfdiI__4E88ABD4");
    }
}

