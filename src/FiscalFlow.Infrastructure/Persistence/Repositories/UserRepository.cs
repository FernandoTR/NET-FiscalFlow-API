using FiscalFlow.Domain;
using FiscalFlow.Domain.Interfaces;
using FiscalFlow.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

}
