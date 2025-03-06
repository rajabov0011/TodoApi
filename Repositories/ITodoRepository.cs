using TodoApi.Entities;

namespace TodoApi.Repositories;
public interface ITodoRepository
{
    Task CreateAsync(Todo todo, CancellationToken cancellationToken = default);
    Task<List<Todo>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Todo?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Todo todo);
    Task DeleteAsync(Todo todo);
}