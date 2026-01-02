using FiscalFlow.Application.Interfaces.Logging;
using FiscalFlow.Domain;
using FiscalFlow.Domain.Interfaces.Certificates;
using FiscalFlow.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure.Persistence.Repositories;

public sealed class CertificateRepository : ICertificateRepository
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;

    public CertificateRepository(AppDbContext context,
        ILogService logService)
    {
        _context = context;
        _logService = logService;
    }

    public async Task<Certificate?> GetActiveByUserIdAsync(Guid userId)
    {
        return await _context.Certificates.FirstOrDefaultAsync(c => c.UserId == userId && c.IsActive);
    }

    public async Task<Certificate?> GetByIdAsync(Guid certificateId)
    {
        return await _context.Certificates.FirstOrDefaultAsync(c => c.CertificateId == certificateId);
    }

    public async Task<bool> AddAsync(Certificate certificate)
    {
        try
        {
            await _context.Certificates.AddAsync(certificate);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(AddAsync), ex);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Certificate certificate)
    {        
        try
        {
            _context.Certificates.Update(certificate);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(UpdateAsync), ex);
            return false;
        }
    }
}

