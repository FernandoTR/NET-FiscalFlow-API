using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class StampMovementConfiguration : IEntityTypeConfiguration<StampMovement>
{
    public void Configure(EntityTypeBuilder<StampMovement> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__StampMov__3214EC07292A7110");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.Cfdi)
               .WithMany(c => c.StampMovements)
               .HasConstraintName("FK__StampMove__CfdiI__619B8048");

        builder.HasOne(x => x.User)
               .WithMany(u => u.StampMovements)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__StampMove__UserI__60A75C0F");
    }
}

