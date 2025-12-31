using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalFlow.Application.DTOs;

public sealed class AuthResult
{
    public string AccessToken { get; init; } = default!;
    public string RefreshToken { get; init; } = default!;
    public string TokenType { get; init; } = "Bearer";
    public DateTime IssuedAt { get; init; }
    public DateTime ExpiresAt { get; init; }
}
