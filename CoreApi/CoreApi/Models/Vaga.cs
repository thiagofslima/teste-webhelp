using System.ComponentModel.DataAnnotations;

namespace CoreApi.Models;

public class Vaga
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O título da vaga é obrigatório")]
    [MaxLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
    public string Titulo { get; set; }
    public string Descricao { get; set; }
}
