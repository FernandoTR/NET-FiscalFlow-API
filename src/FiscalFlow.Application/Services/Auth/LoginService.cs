using FiscalFlow.Application.Services.Security;
using FiscalFlow.Domain;
using FiscalFlow.Domain.Interfaces.Users;

namespace FiscalFlow.Application.Services.Auth;

public class LoginService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasherService _passwordHasher;

    public LoginService(
        IUserRepository userRepository,
        PasswordHasherService passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
            throw new UnauthorizedAccessException("Credenciales inválidas");

        var isValidPassword = _passwordHasher.Verify(
            user.PasswordHash,
            password);

        if (!isValidPassword)
            throw new UnauthorizedAccessException("Credenciales inválidas");

        return user;
    }
}
