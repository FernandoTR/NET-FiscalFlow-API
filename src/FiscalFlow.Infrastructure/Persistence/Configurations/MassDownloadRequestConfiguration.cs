using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class MassDownloadRequestConfiguration : IEntityTypeConfiguration<MassDownloadRequest>
{
    public void Configure(EntityTypeBuilder<MassDownloadRequest> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__MassDown__3214EC07A2F80CC1");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.User)
               .WithMany(u => u.MassDownloadRequests)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__MassDownl__UserI__74AE54BC");
    }
}

