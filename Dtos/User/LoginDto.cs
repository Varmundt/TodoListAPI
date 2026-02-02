using System.ComponentModel.DataAnnotations;

namespace todoAPI.Dtos.User;

public class LoginDto
{
    [Required(ErrorMessage = "O Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email Inválido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A senha é obrigaória")]
    public string Password { get; set; }
}