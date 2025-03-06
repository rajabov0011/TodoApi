using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Entities;

namespace TodoApi.Repositories;

public class TodoRepository(AppDbContext context) : ITodoRepository
{
    public async Task CreateAsync(Todo todo, CancellationToken cancellationToken)
    {
        await context.Todos.AddAsync(todo, cancellationToken);
        await context.SaveChangesAsync();
    }

    public async Task<List<Todo>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken) => 
        await context.Todos
            .Where(t => t.UserId == userId)
            .ToListAsync(cancellationToken);

    public async Task<Todo?> GetByIdAsync(Guid todoId, CancellationToken cancellationToken) =>
        await context.Todos.Where(t => t.Id == todoId).FirstOrDefaultAsync(cancellationToken);

    public async Task UpdateAsync(Todo todo)
    {
        context.Todos.Update(todo);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Todo todo)
    {
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();
    }
}