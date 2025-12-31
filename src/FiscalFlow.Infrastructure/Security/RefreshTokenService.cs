using FiscalFlow.Application.Interfaces;
using FiscalFlow.Domain;
using FiscalFlow.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace FiscalFlow.Infrastructure.Security;

public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly AppDbContext _context;

    public RefreshTokenService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AuthToken> CreateAsync(User user)
    {
        var token = new AuthToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = GenerateSecureToken(),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        _context.AuthTokens.Add(token);
        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<AuthToken?> ValidateAsync(string refreshToken)
    {
        return await _context.AuthTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x =>
                x.Token == refreshToken &&
                !x.IsRevoked &&
                x.ExpiresAt > DateTime.UtcNow);
    }

    public async Task RevokeAsync(AuthToken token)
    {
        token.IsRevoked = true;
        await _context.SaveChangesAsync();
    }

    private static string GenerateSecureToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }
}
