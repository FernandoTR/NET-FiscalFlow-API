using FiscalFlow.Application.Interfaces.Logging;
using FiscalFlow.Domain.Entities;
using FiscalFlow.Domain.Interfaces.Auth;
using FiscalFlow.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure.Persistence.Repositories;

public sealed class AuthTokenRepository : IAuthTokenRepository
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;

    public AuthTokenRepository(AppDbContext context,
                               ILogService logService)
    {
        _context = context;
        _logService = logService;
    }

    public async Task<AuthToken?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.AuthTokens.FirstOrDefaultAsync(t => t.Token == refreshToken && !t.IsRevoked);
    }

    public async Task<bool> AddAsync(AuthToken token)
    {        
        try
        {
            await _context.AuthTokens.AddAsync(token);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(AddAsync), ex);
            return false;
        }
    }

    public void Revoke(AuthToken token)
    {        
        try
        {
            token.IsRevoked = true;
            _context.AuthTokens.Update(token);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logService.ErrorLog(nameof(Revoke), ex);
        }
    }
}

