using FiscalFlow.Application.DTOs;
using FiscalFlow.Domain;

namespace FiscalFlow.Application.Interfaces.Auth;

public interface IJwtTokenService
{
    JwtTokenResult GenerateToken(User user);
}
