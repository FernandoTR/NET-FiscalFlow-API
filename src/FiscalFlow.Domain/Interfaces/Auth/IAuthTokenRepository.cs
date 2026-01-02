
namespace FiscalFlow.Domain.Interfaces.Auth;

public interface IAuthTokenRepository
{
    Task<AuthToken?> GetByRefreshTokenAsync(string refreshToken);
    Task<bool> AddAsync(AuthToken token);
    void Revoke(AuthToken token);
}

