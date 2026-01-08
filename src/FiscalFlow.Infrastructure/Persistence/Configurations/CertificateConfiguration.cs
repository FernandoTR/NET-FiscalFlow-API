using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> builder)
    {
        builder.HasKey(x => x.CertificateId)
               .HasName("PK__Certific__BBF8A7C1EB9B1929");

        builder.Property(x => x.CertificateId)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);

        builder.HasOne(x => x.User)
               .WithMany(u => u.Certificates)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__Certifica__UserI__08B54D69");
    }
}

