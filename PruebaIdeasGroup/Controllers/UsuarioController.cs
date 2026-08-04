using Microsoft.AspNetCore.Mvc;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Application.Ports.In;

namespace PruebaIdeasGroup.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
    {
        var usuarios = await _service.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDto?>> GetById(int id)
    {
        var usuario = await _service.GetByIdAsync(id);
        if (usuario is null)
            return NotFound();
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Create(CreateUsuarioDto dto)
    {
        try
        {
            var usuario = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUsuarioDto dto)
    {
            var updatedUsuario = await _service.UpdateAsync(id, dto);
            if (!updatedUsuario)
                return NotFound();
            return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedUsuario = await _service.DeleteAsync(id);
        if (!deletedUsuario)
            return NotFound();
        return NoContent();
    }
}