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
public class UsuarioController : ControllerBase
{
    private CoreContext _context;
    private IMapper _mapper;

    public UsuarioController(CoreContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma usuario ao banco de dados
    /// </summary>
    /// <param name="usuarioDto">Objeto com os campos necessários para criação de uma usuario</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarUsuario([FromBody] UsuarioDto usuarioDto)
    {
        Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        // Retorna o que foi salvo, inclusive o caminho.
        return CreatedAtAction(nameof(RecuperaUsuarioPorId), new { id = usuario.Id }, usuario);
    }

    [HttpGet]
    public IEnumerable<UsuarioDto> RecuperaUsuarios([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<UsuarioDto>>(_context.Usuarios.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaUsuarioPorId(int id)
    {
        // Busca utilizando LINQ.
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null)
            return NotFound();

        var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
        return Ok(usuario);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaUsuario(int id, [FromBody] UsuarioDto usuarioDto)
    {
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null)
            return NotFound();

        _mapper.Map(usuarioDto, usuario);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaUsuario(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Id == id);
        if (usuario == null)
            return NotFound();

        _context.Remove(usuario);
        _context.SaveChanges();
        return NoContent();

    }
}
