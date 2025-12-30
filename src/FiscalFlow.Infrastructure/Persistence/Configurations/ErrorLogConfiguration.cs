using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

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