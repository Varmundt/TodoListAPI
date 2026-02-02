using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoAPI.Data;
using todoAPI.Dtos.Todo;
using todoAPI.Models;

namespace todoAPI.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TodoController(ApplicationDbContext context)
    {
        _context = context;
    }

[HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemResponseDto>>> GetAll()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var tasks = await _context.TodoItems
            .Where(t => t.UserId == userId)
            .Select(t => new TodoItemResponseDto
            {
                Id = t.Id,
                TaskName = t.TaskName,
                IsDone = t.IsDone
            })
            .ToListAsync();
        
        return Ok(tasks);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemResponseDto>> GetById(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var task = await _context.TodoItems.FindAsync(id);
        
        if (task == null)
            return NotFound(new { message = "Tarefa não encontrada" });
        
        if (task.UserId != userId)
            return Forbid();
    
        var response = new TodoItemResponseDto
        {
            Id = task.Id,
            TaskName = task.TaskName,
            IsDone = task.IsDone
        };
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<TodoItemResponseDto>> Create([FromBody] TodoItemDto todoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
    
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    
        var todoItem = new TodoItem
        {
            TaskName = todoDto.TaskName,
            IsDone = todoDto.IsDone,
            UserId = userId
        };

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        var response = new TodoItemResponseDto
        {
            Id = todoItem.Id,
            TaskName = todoItem.TaskName,
            IsDone = todoItem.IsDone
        };

        return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, response);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoItemDto todoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
    
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var task = await _context.TodoItems.FindAsync(id);
        
        if (task == null)
            return NotFound(new { message = "Tarefa não encontrada" });
        
        if (task.UserId != userId)
            return Forbid();

        task.TaskName = todoDto.TaskName;
        task.IsDone = todoDto.IsDone;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var task = await _context.TodoItems.FindAsync(id);
        
        if (task == null)
            return NotFound(new { message = "Tarefa não encontrada" });
        
        if (task.UserId != userId)
            return Forbid();

        _context.TodoItems.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}