
namespace FiscalFlow.Application.DTOs.Auth;

public sealed record AuthResponseDto(
     string AccessToken,
     string TokenType,
     DateTime IssuedAt,
     DateTime ExpiresAt,
     string RefreshToken
);
