using System.Collections;
using Dtos;
using TodoApi.Entities;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<TodoDto> CreateAsync(Guid userId, CreateTodo createTodo);
    Task<IEnumerable<TodoDto>> GetAllByUserIdAsync(Guid userId);
    Task<TodoDto> GetByIdAsync(Guid userId, Guid id);
    Task<bool> UpdateAsync(Guid userId, Guid id, UpdateTodo updateTodo);
    Task<bool> DeleteAsync(Guid userId, Guid id);
}