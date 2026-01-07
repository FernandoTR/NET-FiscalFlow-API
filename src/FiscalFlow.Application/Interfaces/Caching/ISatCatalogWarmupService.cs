

namespace FiscalFlow.Application.Interfaces.Caching;

public interface ISatCatalogWarmupService
{
    Task WarmupAsync(CancellationToken ct);
}

