using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public sealed class SatCatalogConfiguration : IEntityTypeConfiguration<SatCatalog>
{
    public void Configure(EntityTypeBuilder<SatCatalog> builder)
    {
        builder.ToTable("SatCatalog");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(255);

        builder.Property(x => x.CfdiVersion)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasMany(x => x.Items)
            .WithOne(i => i.Catalog)
            .HasForeignKey(i => i.SatCatalogId);
    }
}
