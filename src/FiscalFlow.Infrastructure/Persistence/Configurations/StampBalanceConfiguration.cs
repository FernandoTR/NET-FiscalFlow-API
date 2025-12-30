using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class StampBalanceConfiguration : IEntityTypeConfiguration<StampBalance>
{
    public void Configure(EntityTypeBuilder<StampBalance> builder)
    {
        builder.HasKey(x => x.UserId)
               .HasName("PK__StampBal__1788CC4C7B76C78B");

        builder.Property(x => x.UserId)
               .ValueGeneratedNever();

        builder.Property(x => x.UpdatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.User)
               .WithOne(u => u.StampBalance)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__StampBala__UserI__5BE2A6F2");
    }
}

