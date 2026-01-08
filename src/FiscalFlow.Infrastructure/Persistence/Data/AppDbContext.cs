using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure.Persistence.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<AuthToken> AuthTokens { get; set; }

    public virtual DbSet<BatchItem> BatchItems { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Cfdi> Cfdis { get; set; }

    public virtual DbSet<CfdiBatch> CfdiBatches { get; set; }

    public virtual DbSet<CfdiPdf> CfdiPdfs { get; set; }

    public virtual DbSet<CfdiStatusHistory> CfdiStatusHistories { get; set; }

    public virtual DbSet<Cfdi> CfdiXmls { get; set; }

    public virtual DbSet<EmailLog> EmailLogs { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<MassDownloadFile> MassDownloadFiles { get; set; }

    public virtual DbSet<MassDownloadRequest> MassDownloadRequests { get; set; }

    public virtual DbSet<SatCatalog> SatCatalogs { get; set; }

    public virtual DbSet<SatCatalogItem> SatCatalogItems { get; set; }

    public virtual DbSet<SatCatalogRule> SatCatalogRules { get; set; }

    public virtual DbSet<StampBalance> StampBalances { get; set; }

    public virtual DbSet<StampMovement> StampMovements { get; set; }

    public virtual DbSet<User> Users { get; set; }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
