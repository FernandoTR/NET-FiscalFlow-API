using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(EntityTypeBuilder<ErrorLog> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_LogError");

        builder.Property(e => e.LogDate).HasColumnType("datetime");
        builder.Property(e => e.MethodName).HasMaxLength(250);
    }
}