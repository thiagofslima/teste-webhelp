﻿using CoreApi.Models;

namespace CoreApi.Data.Dtos;

public class EmpresaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public virtual ICollection<Tecnologia>? Tecnologias { get; set; } = null!;
}
