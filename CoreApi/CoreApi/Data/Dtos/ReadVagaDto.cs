using System.ComponentModel.DataAnnotations;

namespace CoreApi.Data.Dtos;

public class ReadVagaDto
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
}
