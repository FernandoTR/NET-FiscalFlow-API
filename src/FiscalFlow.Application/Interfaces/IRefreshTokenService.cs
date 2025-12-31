using FiscalFlow.Domain;

namespace FiscalFlow.Application.Interfaces;

public interface IRefreshTokenService
{
    Task<AuthToken> CreateAsync(User user);
    Task<AuthToken?> ValidateAsync(string refreshToken);
    Task RevokeAsync(AuthToken token);
}
