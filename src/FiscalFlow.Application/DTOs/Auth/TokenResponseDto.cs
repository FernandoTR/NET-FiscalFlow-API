
namespace FiscalFlow.Application.DTOs.Auth;

public record TokenResponseDto(
     string AccessToken,
     string TokenType,
     DateTime IssuedAt,
     DateTime ExpiresAt,
     string RefreshToken
);
