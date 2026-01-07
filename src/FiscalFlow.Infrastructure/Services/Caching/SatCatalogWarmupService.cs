using FiscalFlow.Application.Interfaces.Caching;
using FiscalFlow.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FiscalFlow.Infrastructure.Services.Caching;

public sealed class SatCatalogWarmupService : ISatCatalogWarmupService
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;

    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(12);

    public SatCatalogWarmupService(AppDbContext appDbContext, IMemoryCache cache)
    {
        _context = appDbContext;
        _cache = cache;
    }

    public async Task WarmupAsync(CancellationToken ct)
    {
        // 1️ Cargar items de catálogo
        var items = await _context.SatCatalogItems
            .Where(x => x.IsActive)
            .Select(x => new
            {
                CatalogCode = x.Catalog.Code,
                x.KeyCode
            })
            .ToListAsync(ct);

        foreach (var item in items)
        {
            var cacheKey = $"SAT:{item.CatalogCode}:{item.KeyCode}";
            _cache.Set(cacheKey, true, CacheDuration);
        }

        // 2️ Cargar reglas cruzadas
        var rules = await _context.SatCatalogRules
            .Where(r => r.IsAllowed)
            .Select(r => new
            {
                r.CatalogCode,
                r.ItemKey,
                r.AppliesToCatalog,
                r.AppliesToKey
            })
            .ToListAsync(ct);

        foreach (var rule in rules)
        {
            var cacheKey =
                $"SAT_RULE:{rule.CatalogCode}:{rule.ItemKey}:{rule.AppliesToCatalog}:{rule.AppliesToKey}";

            _cache.Set(cacheKey, true, CacheDuration);
        }
    }
}

