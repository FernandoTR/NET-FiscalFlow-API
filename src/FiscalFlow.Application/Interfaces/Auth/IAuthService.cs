using FiscalFlow.Application.DTOs.Auth;

namespace FiscalFlow.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);
}