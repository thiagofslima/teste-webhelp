using System.ComponentModel.DataAnnotations;

namespace CoreApi.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo de Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Mínino de 3 caracteres.")]
    [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo de Email é obrigatório")]
    [MinLength(3, ErrorMessage = "Mínino de 3 caracteres para Email.")]
    [MaxLength(25, ErrorMessage = "Máximo de 25 caracteres para Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo de Senha é obrigatório")]
    [MinLength(3, ErrorMessage = "Mínino de 3 caracteres para Senha.")]
    [MaxLength(25, ErrorMessage = "Máximo de 25 caracteres para Senha.")]
    public string Senha { get; set; }
}
