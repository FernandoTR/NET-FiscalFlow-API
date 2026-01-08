using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class CfdiBatchConfiguration : IEntityTypeConfiguration<CfdiBatch>
{
    public void Configure(EntityTypeBuilder<CfdiBatch> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__CfdiBatc__3214EC075B40917A");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.User)
               .WithMany(u => u.CfdiBatches)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__CfdiBatch__UserI__66603565");
    }
}
