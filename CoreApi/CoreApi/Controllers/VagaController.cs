using CoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VagaController : ControllerBase
{
    private static List<Vaga> vagas = new List<Vaga>();

    [HttpPost]
    public IActionResult AdicionarVaga([FromBody] Vaga vaga)
    {
        vagas.Add(vaga);
        // Retorna o que foi salvo, inclusive o caminho.
        return CreatedAtAction(nameof(RecuperaVagaPorId), new { id = vaga.Id }, vaga);
    }

    [HttpGet]
    public IEnumerable<Vaga> RecuperaVagas([FromQuery]int skip = 0, [FromQuery] int take = 50)
    {
        return vagas.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaVagaPorId(int id)
    {
        // Busca utilizando LINQ.
        var vaga = vagas.FirstOrDefault(vaga => vaga.Id == id);
        if (vaga == null)
            return NotFound();

        return Ok(vaga);
    }
}
