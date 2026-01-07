namespace FiscalFlow.Domain.Interfaces.SatCatalog;

public interface ISatCatalogRepository
{
    Task<bool> CatalogItemExistsAsync(
        string catalogCode,
        string key,
        CancellationToken ct);

    Task<bool> IsRuleAllowedAsync(
        string catalogCode,
        string key,
        string appliesToCatalog,
        string appliesToKey,
        CancellationToken ct);
}

