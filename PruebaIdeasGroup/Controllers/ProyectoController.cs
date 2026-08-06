namespace PruebaIdeasGroup.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Application.Ports.In;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProyectoController : ControllerBase
{
    private readonly IProyectoService _service;

    public ProyectoController(IProyectoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProyectoDto>>> GetAll()
    {
        var proyectos = await _service.GetAllAsync();
        return Ok(proyectos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProyectoDto>> GetById(int id)
    {
        var proyecto = await _service.GetByIdAsync(id);
        if (proyecto is null)
            return NotFound();

        return Ok(proyecto);
    }

    [HttpPost]
    public async Task<ActionResult<ProyectoDto>> Create([FromBody] CreateProyectoDto dto)
    {
        try
        {
            var proyecto = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = proyecto.Id }, proyecto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProyectoDto dto)
    {
        try
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}