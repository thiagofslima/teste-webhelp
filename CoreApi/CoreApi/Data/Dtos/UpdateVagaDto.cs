using System.ComponentModel.DataAnnotations;

namespace CoreApi.Data.Dtos;

public class UpdateVagaDto
{
    [Required(ErrorMessage = "O título da vaga é obrigatório")]
    [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
    public string Titulo { get; set; }
    public string Descricao { get; set; }
}
