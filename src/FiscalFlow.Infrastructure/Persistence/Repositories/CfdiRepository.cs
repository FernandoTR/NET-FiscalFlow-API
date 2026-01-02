using FiscalFlow.Application.Interfaces.Logging;
using FiscalFlow.Domain;
using FiscalFlow.Domain.Interfaces.Cfdis;
using FiscalFlow.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure.Persistence.Repositories;

public sealed class CfdiRepository : ICfdiRepository
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;

    public CfdiRepository(AppDbContext context,
        ILogService logService)
    {
        _context = context;
        _logService = logService;
    }

    public async Task<Cfdi?> GetByIdAsync(Guid id)
    {
        return await _context.Cfdis
            .Include(c => c.CfdiXmls)
            .Include(c => c.CfdiPdfs)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> AddAsync(Cfdi cfdi)
    {       
        try
        {
            await _context.Cfdis.AddAsync(cfdi);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(AddAsync), ex);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Cfdi cfdi)
    {        
        try
        {
            _context.Cfdis.Update(cfdi); ;
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(UpdateAsync), ex);
            return false;
        }
    }
}
