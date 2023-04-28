using CoreApi.Models;

namespace CoreApi.Data.Dtos;

public class CandidatoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public virtual ICollection<Vaga>? Vagas { get; set; }
}
