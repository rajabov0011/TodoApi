using TodoApi.Entities;

namespace TodoApi.Repositories;
public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}