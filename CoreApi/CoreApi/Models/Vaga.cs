using System.ComponentModel.DataAnnotations;

namespace CoreApi.Models;

public class Vaga
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo de Título é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
    public string Titulo { get; set; }
    public string Descricao { get; set; }
}
