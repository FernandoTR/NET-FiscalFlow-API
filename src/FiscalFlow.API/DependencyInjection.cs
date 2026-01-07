using FiscalFlow.Application.Interfaces.SatCatalog;
using FiscalFlow.Infrastructure.Services.SatCatalog;
using FiscalFlow.Infrastructure.Services.Hosting;
using FiscalFlow.Infrastructure.Services.MemoryCache;
using Microsoft.Extensions.Caching.Memory;

namespace FiscalFlow.API;

public static class DependencyInjection
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        // Decorator MemoryCache for Sat Catalog
        builder.Services.AddScoped<ISatCatalogService>(sp =>
        {
            var baseService = sp.GetRequiredService<SatCatalogService>();
            var cache = sp.GetRequiredService<IMemoryCache>();

            return new CachedSatCatalogService(baseService, cache);
        });

        // Warmup for Sat Catalog
        builder.Services.AddHostedService<SatCatalogWarmupHostedService>();

    }
}
