using System.Collections;
using Dtos;
using TodoApi.Entities;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService(ITodoRepository todoRepository) : ITodoService
{
    public async Task<TodoDto> CreateAsync(Guid userId, CreateTodo createTodo)
    {
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Title = createTodo.Title,
            Description = createTodo.Description,
            IsCompleted = false,
            UserId = userId
        };

        await todoRepository.CreateAsync(todo);
        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            UserId = todo.UserId
        };
    }

    public async Task<IEnumerable<TodoDto>> GetAllByUserIdAsync(Guid userId)
    {
        var todos = await todoRepository.GetAllByUserAsync(userId);
        return todos.Select(todo => new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            UserId = todo.UserId
        }).ToList();
    }

    public async Task<TodoDto> GetByIdAsync(Guid userId, Guid id)
    {
        var todo = await todoRepository.GetByIdAsync(id);

        if (todo == null || todo.UserId != userId)
            throw new KeyNotFoundException("Todo not found");

        return new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            UserId = todo.UserId
        };
    }

    public async Task<bool> UpdateAsync(Guid userId, Guid id, UpdateTodo updateTodo)
    {
        var todo = await todoRepository.GetByIdAsync(id);

        if (todo == null || todo.UserId != userId)
            throw new KeyNotFoundException("Todo not found");

        todo.Title = updateTodo.Title;
        todo.Description = updateTodo.Description;
        todo.IsCompleted = updateTodo.IsCompleted;

        await todoRepository.UpdateAsync(todo);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid userId, Guid id)
    {
        var todo = await todoRepository.GetByIdAsync(id);

        if (todo == null || todo.UserId != userId)
            throw new KeyNotFoundException("Todo not found");

        await todoRepository.DeleteAsync(todo);
        return true;
    }
}