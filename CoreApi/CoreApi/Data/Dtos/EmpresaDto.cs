using CoreApi.Models;

namespace CoreApi.Data.Dtos;

public class EmpresaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int TecnologiaId { get; set; }
    public virtual ICollection<Tecnologia>? Tecnologias { get; set; } = null!;
}
