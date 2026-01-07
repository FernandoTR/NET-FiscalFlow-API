using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiscalFlow.Infrastructure.Persistence.Configurations;

public sealed class SatCatalogItemConfiguration : IEntityTypeConfiguration<SatCatalogItem>
{
    public void Configure(EntityTypeBuilder<SatCatalogItem> builder)
    {
        builder.ToTable("SatCatalogItem");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.KeyCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => new { x.SatCatalogId, x.KeyCode })
            .IsUnique();

        builder.HasIndex(x => new { x.KeyCode, x.StartDate, x.EndDate });

        builder.HasOne(x => x.Catalog)
            .WithMany(c => c.Items)
            .HasForeignKey(x => x.SatCatalogId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
