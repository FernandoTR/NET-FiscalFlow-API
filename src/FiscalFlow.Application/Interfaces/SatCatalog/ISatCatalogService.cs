
namespace FiscalFlow.Application.Interfaces.SatCatalog;

public interface ISatCatalogService
{
    Task<bool> ExistsAsync(string catalogCode, string key, CancellationToken ct);
    Task<bool> IsCombinationAllowedAsync(
        string catalogCode,
        string key,
        string appliesToCatalog,
        string appliesToKey,
        CancellationToken ct);
}

