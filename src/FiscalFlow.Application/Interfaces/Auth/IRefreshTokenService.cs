using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Application.Interfaces.Auth;

public interface IRefreshTokenService
{
    Task<AuthToken> CreateAsync(User user);
    Task<AuthToken?> ValidateAsync(string refreshToken);
    Task RevokeAsync(AuthToken token);
}
