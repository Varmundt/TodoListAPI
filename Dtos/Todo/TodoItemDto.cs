using System.ComponentModel.DataAnnotations;

namespace todoAPI.Dtos.Todo;

public class TodoItemDto
{
    [Required(ErrorMessage = "o nome da tarefa é obrigatório")]
    [MinLength(1, ErrorMessage = "O nome da tarefa não pode estar vazio")]
    [MaxLength(200, ErrorMessage = "O nome da tarefa deve ter no máximo 200 caracteres")]
    public string TaskName { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}