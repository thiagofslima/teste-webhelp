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
    public IActionResult AdicionarVaga([FromBody] CreateVagaDto vagaDto)
    {
        Vaga vaga = _mapper.Map<Vaga>(vagaDto);
        _context.Vagas.Add(vaga);
        _context.SaveChanges();
        // Retorna o que foi salvo, inclusive o caminho.
        return CreatedAtAction(nameof(RecuperaVagaPorId), new { id = vaga.Id }, vaga);
    }

    [HttpGet]
    public IEnumerable<ReadVagaDto> RecuperaVagas([FromQuery]int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadVagaDto>>(_context.Vagas.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaVagaPorId(int id)
    {
        // Busca utilizando LINQ.
        var vaga = _context.Vagas.FirstOrDefault(vaga => vaga.Id == id);
        if (vaga == null)
            return NotFound();

        var vagaDto = _mapper.Map<ReadVagaDto>(vaga);
        return Ok(vaga);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaVaga(int id, [FromBody] UpdateVagaDto vagaDto)
    {
        var vaga = _context.Vagas.FirstOrDefault(vaga => vaga.Id == id);
        if (vaga == null)
            return NotFound();

        _mapper.Map(vagaDto, vaga);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaVagaParcial(int id, JsonPatchDocument<UpdateVagaDto> patch)
    {
        var vaga = _context.Vagas.FirstOrDefault(vaga => vaga.Id == id);
        if (vaga == null)
            return NotFound();

        var vagaParaAtualizar = _mapper.Map<UpdateVagaDto>(vaga);
        patch.ApplyTo(vagaParaAtualizar, ModelState);

        if (!TryValidateModel(vagaParaAtualizar))
            return ValidationProblem(ModelState);

        _mapper.Map(vagaParaAtualizar, vaga);
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
