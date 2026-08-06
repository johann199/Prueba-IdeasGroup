namespace PruebaIdeasGroup.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Application.Ports.In;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TareaController : ControllerBase
{
    private readonly ITareaService _service;

    public TareaController(ITareaService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TareaDto>> GetById(int id)
    {
        var tarea = await _service.GetByIdAsync(id);
        if (tarea is null)
            return NotFound();

        return Ok(tarea);
    }

    [HttpGet("columna/{columnaId}")]
    public async Task<ActionResult<IEnumerable<TareaDto>>> GetByColumna(int columnaId)
    {
        var tareas = await _service.GetByColumnaIdAsync(columnaId);
        return Ok(tareas);
    }

    [HttpPost]
    public async Task<ActionResult<TareaDto>> Create([FromBody] CreateTareaDto dto)
    {
        try
        {
            var tarea = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = tarea.Id }, tarea);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTareaDto dto)
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

    [HttpPost("{id}/responsables")]
    public async Task<IActionResult> AddResponsable(int id, [FromBody] AddResponsableDto dto)
    {
        try
        {
            await _service.AddResponsableAsync(id, dto.UsuarioId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
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