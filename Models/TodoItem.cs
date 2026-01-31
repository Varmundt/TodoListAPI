using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace todoAPI.Models;

public class TodoItem
{
    public int Id { get; set; }
    [Required, MaxLength(200)]
    public string TaskName { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
}