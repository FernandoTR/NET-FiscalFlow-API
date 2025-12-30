using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class EmailLogConfiguration : IEntityTypeConfiguration<EmailLog>
{
    public void Configure(EntityTypeBuilder<EmailLog> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__EmailLog__3214EC0787196443");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.SentAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.Cfdi)
               .WithMany(c => c.EmailLogs)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__EmailLog__CfdiId__6FE99F9F");
    }
}

