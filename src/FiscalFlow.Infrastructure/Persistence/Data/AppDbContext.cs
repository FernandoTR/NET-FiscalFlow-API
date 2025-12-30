using System;
using System.Collections.Generic;
using FiscalFlow.Domain;
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

    public virtual DbSet<AuthToken> AuthTokens { get; set; }

    public virtual DbSet<BatchItem> BatchItems { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Cfdi> Cfdis { get; set; }

    public virtual DbSet<CfdiBatch> CfdiBatches { get; set; }

    public virtual DbSet<CfdiPdf> CfdiPdfs { get; set; }

    public virtual DbSet<CfdiStatusHistory> CfdiStatusHistories { get; set; }

    public virtual DbSet<CfdiXml> CfdiXmls { get; set; }

    public virtual DbSet<EmailLog> EmailLogs { get; set; }

    public virtual DbSet<MassDownloadFile> MassDownloadFiles { get; set; }

    public virtual DbSet<MassDownloadRequest> MassDownloadRequests { get; set; }

    public virtual DbSet<StampBalance> StampBalances { get; set; }

    public virtual DbSet<StampMovement> StampMovements { get; set; }

    public virtual DbSet<User> Users { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AuthToke__3214EC079412461B");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.AuthTokens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuthToken__UserI__3E52440B");
        });

        modelBuilder.Entity<BatchItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BatchIte__3214EC07B445316F");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BatchItem__Batch__6A30C649");

            entity.HasOne(d => d.Cfdi).WithMany(p => p.BatchItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BatchItem__CfdiI__6B24EA82");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7C1EB9B1929");

            entity.Property(e => e.CertificateId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.Certificates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Certifica__UserI__08B54D69");
        });

        modelBuilder.Entity<Cfdi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cfdi__3214EC07C9F4101B");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.Cfdis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cfdi__UserId__49C3F6B7");
        });

        modelBuilder.Entity<CfdiBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CfdiBatc__3214EC075B40917A");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.CfdiBatches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CfdiBatch__UserI__66603565");
        });

        modelBuilder.Entity<CfdiPdf>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CfdiPdf__3214EC077BD0039A");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Cfdi).WithMany(p => p.CfdiPdfs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CfdiPdf__CfdiId__5812160E");
        });

        modelBuilder.Entity<CfdiStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CfdiStat__3214EC07806CC8C9");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Cfdi).WithMany(p => p.CfdiStatusHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CfdiStatu__CfdiI__4E88ABD4");
        });

        modelBuilder.Entity<CfdiXml>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CfdiXml__D14A66895961B32B");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Cfdi).WithMany(p => p.CfdiXmls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CfdiXml__CfdiId__534D60F1");
        });

        modelBuilder.Entity<EmailLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailLog__3214EC0787196443");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SentAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Cfdi).WithMany(p => p.EmailLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmailLog__CfdiId__6FE99F9F");
        });

        modelBuilder.Entity<MassDownloadFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MassDown__3214EC073AFD2AE7");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Request).WithMany(p => p.MassDownloadFiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MassDownl__Reque__787EE5A0");
        });

        modelBuilder.Entity<MassDownloadRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MassDown__3214EC07A2F80CC1");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.MassDownloadRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MassDownl__UserI__74AE54BC");
        });

        modelBuilder.Entity<StampBalance>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__StampBal__1788CC4C7B76C78B");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.User).WithOne(p => p.StampBalance)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StampBala__UserI__5BE2A6F2");
        });

        modelBuilder.Entity<StampMovement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StampMov__3214EC07292A7110");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Cfdi).WithMany(p => p.StampMovements).HasConstraintName("FK__StampMove__CfdiI__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.StampMovements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StampMove__UserI__60A75C0F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC075204049E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
