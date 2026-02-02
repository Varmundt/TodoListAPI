using System.ComponentModel.DataAnnotations;

namespace todoAPI.Dtos.User;

public class RegisterDto
{
    [Required(ErrorMessage = "O nome de usuário é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome de usuário deve conter no mínimo 3 caracteres")]
    [MaxLength(50, ErrorMessage = "O nome de usuário deve conter no máximo 50 caracteres")]
    public string Username { get; set; }
    [Required(ErrorMessage = "O Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email Inválido")]
    [MaxLength(100, ErrorMessage = "O Email deve conter no máximo 100 caracteres")]
    public string Email { get; set; }
    [Required(ErrorMessage = "A Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
    [MaxLength(100, ErrorMessage = "A senha deve conter no máximo 100 caracteres")]
    public string Password { get; set; }
}