using FiscalFlow.Application.DTOs.Auth;
using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Application.Interfaces.Auth;

public interface IJwtTokenService
{
    JwtTokenDto GenerateToken(User user);
}
