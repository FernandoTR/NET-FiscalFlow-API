using FiscalFlow.Application.DTOs;
using FiscalFlow.Domain;

namespace FiscalFlow.Application.Interfaces;

public interface IJwtTokenService
{
    JwtTokenResult GenerateToken(User user);
}
