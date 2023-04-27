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
public class EmpresaController : ControllerBase
{
    private CoreContext _context;
    private IMapper _mapper;

    public EmpresaController(CoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma empresa ao banco de dados
    /// </summary>
    /// <param name="empresaDto">Objeto com os campos necessários para criação de uma empresa</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarEmpresa([FromBody] EmpresaDto empresaDto)
    {
        Empresa empresa = _mapper.Map<Empresa>(empresaDto);
        _context.Empresas.Add(empresa);
        _context.SaveChanges();
        // Retorna o que foi salvo, inclusive o caminho.
        return CreatedAtAction(nameof(RecuperaEmpresaPorId), new { Id = empresa.Id }, empresa);
    }

    [HttpGet]
    public IEnumerable<EmpresaDto> RecuperaEmpresas([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<EmpresaDto>>(_context.Empresas.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaEmpresaPorId(int id)
    {
        // Busca utilizando LINQ.
        Empresa empresa = _context.Empresas.FirstOrDefault(empresa => empresa.Id == id);
        if (empresa != null)
        {
            EmpresaDto empresaDto = _mapper.Map<EmpresaDto>(empresa);
            return Ok(empresa);
        }

        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaEmpresa(int id, [FromBody] EmpresaDto empresaDto)
    {
        var empresa = _context.Empresas.FirstOrDefault(empresa => empresa.Id == id);
        if (empresa == null)
            return NotFound();

        _mapper.Map(empresaDto, empresa);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaEmpresa(int id)
    {
        var empresa = _context.Empresas.FirstOrDefault(empresa => empresa.Id == id);
        if (empresa == null)
            return NotFound();

        _context.Remove(empresa);
        _context.SaveChanges();
        return NoContent();

    }
}
