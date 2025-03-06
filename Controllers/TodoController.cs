using System.Security.Claims;
using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/todos")]
[ApiController]
[Authorize]
public class TodoController(ITodoService todoService) : ControllerBase
{
    private Guid GetUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            throw new UnauthorizedAccessException("User ID not found");
        }
        return Guid.Parse(userId);
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var todos = await todoService.GetAllByUserIdAsync(GetUserId());
        return Ok(todos);
    }

    [HttpGet("{todoId:guid}")]
    public async Task<IActionResult> GetTodoById(Guid todoId)
    {
        var todo = await todoService.GetByIdAsync(GetUserId(), todoId);
        if (todo == null)
            return NotFound(new { message = "Todo not found" });

        return Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodo createTodo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await todoService.CreateAsync(GetUserId(), createTodo);
        return CreatedAtAction(nameof(GetTodoById), new { todoId = created.Id }, created);
    }

    [HttpPut("{todoId:guid}")]
    public async Task<IActionResult> UpdateTodo(Guid todoId, [FromBody] UpdateTodo updateTodo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await todoService.UpdateAsync(GetUserId(), todoId, updateTodo);
        if (!updated)
            return NotFound(new { message = "Todo not found" });

        return NoContent();
    }

    [HttpDelete("{todoId:guid}")]
    public async Task<IActionResult> DeleteTodoById(Guid todoId)
    {
        var deleted = await todoService.DeleteAsync(GetUserId(), todoId);
        if (!deleted)
            return NotFound(new { message = "Todo not found" });

        return NoContent();
    }
}