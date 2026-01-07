using FiscalFlow.Application.Interfaces.Logging;
using FiscalFlow.Domain.Interfaces.SatCatalog;
using FiscalFlow.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure.Persistence.Repositories;

public sealed class SatCatalogRepository : ISatCatalogRepository
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;

    public SatCatalogRepository(AppDbContext context, ILogService logService)
    {
        _context = context;
        _logService = logService;
    }

    public async Task<bool> CatalogItemExistsAsync(
      string catalogCode,
      string key,
      CancellationToken ct)
    {
        try
        {
            return await _context.SatCatalogItems
           .AnyAsync(x =>
               x.Catalog.Code == catalogCode &&
               x.KeyCode == key &&
               x.IsActive,
               ct);
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(CatalogItemExistsAsync), ex);
            return false;
        }       
    }

    public async Task<bool> IsRuleAllowedAsync(
        string catalogCode,
        string key,
        string appliesToCatalog,
        string appliesToKey,
        CancellationToken ct)
    {
        try
        {
            return await _context.SatCatalogRules
            .AnyAsync(r =>
                r.CatalogCode == catalogCode &&
                r.ItemKey == key &&
                r.AppliesToCatalog == appliesToCatalog &&
                r.AppliesToKey == appliesToKey &&
                r.IsAllowed,
                ct);
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(IsRuleAllowedAsync), ex);
            return false;
        }
       
    }


}
