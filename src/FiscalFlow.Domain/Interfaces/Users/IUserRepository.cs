namespace FiscalFlow.Domain.Interfaces.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);

    Task<bool> AddAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
}
