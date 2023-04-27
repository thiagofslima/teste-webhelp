using CoreApi.Models;

namespace CoreApi.Data.Dtos;

public class TecnologiaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int EmpresaId { get; set; }
    public virtual ICollection<Empresa>? Empresas { get; set; } = null!;
}
