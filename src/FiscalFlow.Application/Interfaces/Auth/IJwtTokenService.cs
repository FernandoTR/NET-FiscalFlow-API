using FiscalFlow.Application.DTOs.Auth;
using FiscalFlow.Domain;

namespace FiscalFlow.Application.Interfaces.Auth;

public interface IJwtTokenService
{
    JwtTokenDto GenerateToken(User user);
}
