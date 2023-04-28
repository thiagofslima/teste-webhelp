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
public class VagaController : ControllerBase
{
    private CoreContext _context;
    private IMapper _mapper;

    public VagaController(CoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma vaga ao banco de dados
    /// </summary>
    /// <param name="vagaDto">Objeto com os campos necessários para criação de uma vaga</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarVaga([FromBody] VagaDto vagaDto)
    {
        Vaga vaga = _mapper.Map<Vaga>(vagaDto);
        _context.Vagas.Add(vaga);
        _context.SaveChanges();
        // Retorna o que foi salvo, inclusive o caminho.
        return CreatedAtAction(nameof(RecuperaVagaPorId), new { Id = vaga.Id }, vaga);
    }

    //[HttpGet]
    //public IEnumerable<VagaDto> RecuperaVagas([FromQuery] int skip = 0, [FromQuery] int take = 50)
    //{
    //    return _mapper.Map<List<VagaDto>>(_context.Vagas.Skip(skip).Take(take));
    //}

    [HttpGet]
    public IEnumerable<VagaDto> RecuperaVagas()
    {
        return _mapper.Map<List<VagaDto>>(_context.Vagas.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaVagaPorId(int id)
    {
        // Busca utilizando LINQ.
        Vaga vaga = _context.Vagas.FirstOrDefault(vaga => vaga.Id == id);
        if (vaga != null)
        {
            VagaDto vagaDto = _mapper.Map<VagaDto>(vaga);
            return Ok(vaga);
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaVaga(int id, [FromBody] VagaDto vagaDto)
    {
        var vaga = _context.Vagas.FirstOrDefault(vaga => vaga.Id == id);
        if (vaga == null)
            return NotFound();

        _mapper.Map(vagaDto, vaga);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaVaga(int id)
    {
        var vaga = _context.Vagas.FirstOrDefault(vaga => vaga.Id == id);
        if (vaga == null)
            return NotFound();

        _context.Remove(vaga);
        _context.SaveChanges();
        return NoContent();

    }
}
