
using FiscalFlow.Application.Interfaces.SatCatalog;
using Microsoft.Extensions.Caching.Memory;

namespace FiscalFlow.Infrastructure.Services.MemoryCache;

public sealed class CachedSatCatalogService : ISatCatalogService
{
    private readonly ISatCatalogService _inner;
    private readonly IMemoryCache _cache;

    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(12);

    public CachedSatCatalogService(ISatCatalogService inner, IMemoryCache cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<bool> ExistsAsync(string catalogCode, string key, CancellationToken ct)
    {
        var cacheKey = $"SAT:{catalogCode}:{key}";

        if (_cache.TryGetValue(cacheKey, out bool exists))
            return exists;

        exists = await _inner.ExistsAsync(catalogCode, key, ct);

        _cache.Set(cacheKey, exists, CacheDuration);

        return exists;
    }

    public async Task<bool> IsCombinationAllowedAsync(
        string catalogCode,
        string key,
        string appliesToCatalog,
        string appliesToKey,
        CancellationToken ct)
    {
        var cacheKey =
            $"SAT_RULE:{catalogCode}:{key}:{appliesToCatalog}:{appliesToKey}";

        if (_cache.TryGetValue(cacheKey, out bool allowed))
            return allowed;

        allowed = await _inner.IsCombinationAllowedAsync(
            catalogCode, key, appliesToCatalog, appliesToKey, ct);

        _cache.Set(cacheKey, allowed, CacheDuration);

        return allowed;
    }
}

