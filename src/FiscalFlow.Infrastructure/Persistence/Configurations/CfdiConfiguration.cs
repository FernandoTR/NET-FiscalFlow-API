using FiscalFlow.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public class CfdiConfiguration : IEntityTypeConfiguration<Cfdi>
{
    public void Configure(EntityTypeBuilder<Cfdi> builder)
    {
        builder.HasKey(x => x.Id)
               .HasName("PK__Cfdi__3214EC07C9F4101B");

        builder.Property(x => x.Id)
               .HasDefaultValueSql("(newid())");

        builder.Property(x => x.CreatedAt)
               .HasDefaultValueSql("(sysdatetime())");

        builder.HasOne(x => x.User)
               .WithMany(u => u.Cfdis)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK__Cfdi__UserId__49C3F6B7");
    }
}

