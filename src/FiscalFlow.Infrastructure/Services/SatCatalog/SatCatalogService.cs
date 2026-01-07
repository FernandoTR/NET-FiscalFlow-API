using FiscalFlow.Application.Interfaces.SatCatalog;
using FiscalFlow.Domain.Interfaces.SatCatalog;

namespace FiscalFlow.Infrastructure.Services.SatCatalog;

public sealed class SatCatalogService : ISatCatalogService
{
    private readonly ISatCatalogRepository _repository;

    public SatCatalogService(ISatCatalogRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> ExistsAsync(string catalogCode, string key, CancellationToken ct)
        => _repository.CatalogItemExistsAsync(catalogCode, key, ct);

    public Task<bool> IsCombinationAllowedAsync(
        string catalogCode,
        string key,
        string appliesToCatalog,
        string appliesToKey,
        CancellationToken ct)
        => _repository.IsRuleAllowedAsync(
            catalogCode, key, appliesToCatalog, appliesToKey, ct);
}
