namespace CoreApi.Models;

public class Empresa
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public virtual ICollection<Tecnologia>? Tecnologias { get; set; } = null!;
}
