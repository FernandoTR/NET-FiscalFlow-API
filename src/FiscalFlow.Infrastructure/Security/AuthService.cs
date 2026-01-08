
using FiscalFlow.Application.DTOs.Auth;
using FiscalFlow.Application.Interfaces.Auth;
using FiscalFlow.Domain.Entities;
using FiscalFlow.Domain.Interfaces.Auth;
using FiscalFlow.Domain.Interfaces.Users;

namespace FiscalFlow.Infrastructure.Security;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthTokenRepository _authTokenRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IAuthTokenRepository authTokenRepository,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _authTokenRepository = authTokenRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }


    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            return null;

        if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
            return null;

        var accessToken = _jwtTokenService.GenerateToken(user);

        var refreshToken = new AuthToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = Guid.NewGuid().ToString("N"),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _authTokenRepository.AddAsync(refreshToken);

        return new AuthResponseDto
        (
            AccessToken: accessToken.AccessToken,
            TokenType: accessToken.TokenType,
            IssuedAt: accessToken.IssuedAt,
            ExpiresAt: accessToken.ExpiresAt,
            RefreshToken: refreshToken.Token
        );
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await _authTokenRepository.GetByRefreshTokenAsync(refreshToken);

        if (storedToken is null ||
            storedToken.IsRevoked ||
            storedToken.ExpiresAt < DateTime.UtcNow)
            return null;

        var user = await _userRepository.GetByIdAsync(storedToken.UserId);

        // revoca el token enviado
        _authTokenRepository.Revoke(storedToken);

        var newRefreshToken = new AuthToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = Guid.NewGuid().ToString("N"),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        var accessToken = _jwtTokenService.GenerateToken(user);

        await _authTokenRepository.AddAsync(newRefreshToken);

        return new AuthResponseDto
         (
             AccessToken: accessToken.AccessToken,
             TokenType: accessToken.TokenType,
             IssuedAt: accessToken.IssuedAt,
             ExpiresAt: accessToken.ExpiresAt,
             RefreshToken: newRefreshToken.Token
         );
    }




}
