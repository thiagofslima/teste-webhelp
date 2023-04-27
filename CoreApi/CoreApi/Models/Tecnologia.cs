namespace CoreApi.Models;

public class Tecnologia
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int EmpresaId { get; set; }
    public virtual ICollection<Empresa>? Empresas { get; set; } = null!;
}
