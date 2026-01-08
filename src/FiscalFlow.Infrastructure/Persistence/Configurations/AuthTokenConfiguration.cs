using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class AuthTokenConfiguration : IEntityTypeConfiguration<AuthToken>
{
    public void Configure(EntityTypeBuilder<AuthToken> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__AuthToke__3214EC079412461B");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.User)
               .WithMany(u => u.AuthTokens)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__AuthToken__UserI__3E52440B");
    }
}

