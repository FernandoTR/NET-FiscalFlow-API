using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public sealed class SatCatalogRuleConfiguration : IEntityTypeConfiguration<SatCatalogRule>
{
    public void Configure(EntityTypeBuilder<SatCatalogRule> builder)
    {
        builder.ToTable("SatCatalogRule");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CatalogCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ItemKey)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.AppliesToCatalog)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.AppliesToKey)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.IsAllowed)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.CatalogCode,
            x.ItemKey,
            x.AppliesToCatalog,
            x.AppliesToKey
        });
    }
}

