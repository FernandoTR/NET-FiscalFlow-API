using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Logs");

        builder.Property(e => e.EventType).HasMaxLength(250);
        builder.Property(e => e.LogDate).HasColumnType("datetime");

        builder.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Logs_Users");
    }
}
