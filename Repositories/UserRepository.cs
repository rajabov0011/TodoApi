using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Entities;

namespace TodoApi.Repositories;
public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task CreateAsync(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken) => 
        await context.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
}