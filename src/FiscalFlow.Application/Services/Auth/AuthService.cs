using FiscalFlow.Application.Interfaces.Auth;
using FiscalFlow.Domain.Interfaces.Auth;
using FiscalFlow.Domain.Interfaces.Users;
using System;

namespace FiscalFlow.Application.Services.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthTokenRepository _authTokenRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly AppDbContext _dbContext;

    public AuthService(
        IUserRepository userRepository,
        IAuthTokenRepository authTokenRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        AppDbContext dbContext)
    {
        _userRepository = userRepository;
        _authTokenRepository = authTokenRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _dbContext = dbContext;
    }



}
