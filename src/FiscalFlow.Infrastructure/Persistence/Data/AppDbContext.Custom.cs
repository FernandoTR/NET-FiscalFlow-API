using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalFlow.Infrastructure.Persistence.Data;

// Archivo separado para mantener y proteger las personalizaciones en OnModelCreating mientras usas Database First.
public partial class AppDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        // Apply all configurations from the current assembly
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Configurar la clase como entidad sin clave
        //modelBuilder.Entity<GetMenuByUserIdDto>().HasNoKey();


    }
}
