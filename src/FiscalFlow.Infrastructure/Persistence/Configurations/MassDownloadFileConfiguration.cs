using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class MassDownloadFileConfiguration : IEntityTypeConfiguration<MassDownloadFile>
{
    public void Configure(EntityTypeBuilder<MassDownloadFile> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__MassDown__3214EC073AFD2AE7");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.HasOne(x => x.Request)
               .WithMany(r => r.MassDownloadFiles)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__MassDownl__Reque__787EE5A0");
    }
}

