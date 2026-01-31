using System.ComponentModel.DataAnnotations;

namespace todoAPI.Dtos.Todo;

public class TodoItemDto
{
    [Required, MaxLength(200)]
    public string TaskName { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}