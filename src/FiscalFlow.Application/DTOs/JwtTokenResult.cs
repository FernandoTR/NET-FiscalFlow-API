
namespace FiscalFlow.Application.DTOs;

public class JwtTokenResult
{
    public string AccessToken { get; init; } = default!;
    public string TokenType { get; init; } = "Bearer";
    public DateTime IssuedAt { get; init; }
    public DateTime ExpiresAt { get; init; }
}
