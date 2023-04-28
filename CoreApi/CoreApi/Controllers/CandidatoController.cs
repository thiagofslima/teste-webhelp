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
public class CandidatoController : ControllerBase
{
    private CoreContext _context;
    private IMapper _mapper;

    public CandidatoController(CoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma candidato ao banco de dados
    /// </summary>
    /// <param name="candidatoDto">Objeto com os campos necessários para criação de uma candidato</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarCandidato([FromBody] CandidatoDto candidatoDto)
    {
        Candidato candidato = _mapper.Map<Candidato>(candidatoDto);
        _context.Candidatos.Add(candidato);
        _context.SaveChanges();
        // Retorna o que foi salvo, inclusive o caminho.
        return CreatedAtAction(nameof(RecuperaCandidatoPorId), new { Id = candidato.Id }, candidato);
    }

    [HttpGet]
    public IEnumerable<CandidatoDto> RecuperaCandidatos([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<CandidatoDto>>(_context.Candidatos.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaCandidatoPorId(int id)
    {
        // Busca utilizando LINQ.
        Candidato candidato = _context.Candidatos.FirstOrDefault(candidato => candidato.Id == id);
        if (candidato != null)
        {
            CandidatoDto candidatoDto = _mapper.Map<CandidatoDto>(candidato);
            return Ok(candidato);
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaCandidato(int id, [FromBody] CandidatoDto candidatoDto)
    {
        var candidato = _context.Candidatos.FirstOrDefault(candidato => candidato.Id == id);
        if (candidato == null)
            return NotFound();

        _mapper.Map(candidatoDto, candidato);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaCandidato(int id)
    {
        var candidato = _context.Candidatos.FirstOrDefault(candidato => candidato.Id == id);
        if (candidato == null)
            return NotFound();

        _context.Remove(candidato);
        _context.SaveChanges();
        return NoContent();

    }
}
