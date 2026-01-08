using FiscalFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure.Persistence.Data;

// DbContext para manejar logs de errores en la base de datos (esto te permite aislar las operaciones de logging del resto de la aplicación)
public class LoggingDbContext : DbContext
{
    public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<ErrorLog> ErrorLogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuraciones adicionales para la tabla de logs
        modelBuilder.Entity<ActivityLog>().ToTable("ActivityLog");
        modelBuilder.Entity<ErrorLog>().ToTable("ErrorLogs");     

    }
}
