namespace todoAPI.Dtos.Todo;

public class TodoItemResponseDto
{
    public int Id { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}