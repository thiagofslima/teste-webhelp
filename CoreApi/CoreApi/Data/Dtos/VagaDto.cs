using CoreApi.Models;
using System.ComponentModel.DataAnnotations;

namespace CoreApi.Data.Dtos;

public class VagaDto
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo de Título é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O título não pode exceder 50 caracteres.")]
    public string Titulo { get; set; }
    [Required(ErrorMessage = "O campo de Descrição é obrigatório.")]
    [MaxLength(100, ErrorMessage = "A Descrição não pode exceder 100 caracteres.")]
    public string Descricao { get; set; }
    public virtual ICollection<Candidato>? Candidatos { get; set; }
}
