using AutoMapper;
using CoreApi.Data;
using CoreApi.Data.Dtos;
using CoreApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TecnologiaController : ControllerBase
{
    private CoreContext _context;
    private IMapper _mapper;

    public TecnologiaController(CoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma tecnologia ao banco de dados
    /// </summary>
    /// <param name="tecnologiaDto">Objeto com os campos necessários para criação de uma tecnologia</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarTecnologia([FromBody] TecnologiaDto tecnologiaDto)
    {
        Tecnologia tecnologia = _mapper.Map<Tecnologia>(tecnologiaDto);
        _context.Tecnologias.Add(tecnologia);
        _context.SaveChanges();
        // Retorna o que foi salvo, inclusive o caminho.
        return CreatedAtAction(nameof(RecuperaTecnologiaPorId), new { id = tecnologia.Id }, tecnologiaDto);
    }

    [HttpGet]
    public IEnumerable<TecnologiaDto> RecuperaTecnologias([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<TecnologiaDto>>(_context.Tecnologias.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaTecnologiaPorId(int id)
    {
        // Busca utilizando LINQ.
        var tecnologia = _context.Tecnologias.FirstOrDefault(tecnologia => tecnologia.Id == id);
        if (tecnologia == null)
            return NotFound();

        var tecnologiaDto = _mapper.Map<TecnologiaDto>(tecnologia);
        return Ok(tecnologia);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaTecnologia(int id, [FromBody] TecnologiaDto tecnologiaDto)
    {
        var tecnologia = _context.Tecnologias.FirstOrDefault(tecnologia => tecnologia.Id == id);
        if (tecnologia == null)
            return NotFound();

        _mapper.Map(tecnologiaDto, tecnologia);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaTecnologia(int id)
    {
        var tecnologia = _context.Tecnologias.FirstOrDefault(tecnologia => tecnologia.Id == id);
        if (tecnologia == null)
            return NotFound();

        _context.Remove(tecnologia);
        _context.SaveChanges();
        return NoContent();

    }
}
