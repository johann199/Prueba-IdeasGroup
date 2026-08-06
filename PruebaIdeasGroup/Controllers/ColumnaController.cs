namespace PruebaIdeasGroup.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Application.Ports.In;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ColumnaController : ControllerBase
{
    private readonly IColumnaService _service;

    public ColumnaController(IColumnaService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ColumnaDto>> GetById(int id)
    {
        var columna = await _service.GetByIdAsync(id);
        if (columna is null)
            return NotFound();

        return Ok(columna);
    }

    [HttpGet("proyecto/{proyectoId}")]
    public async Task<ActionResult<IEnumerable<ColumnaDto>>> GetByProyecto(int proyectoId)
    {
        var columnas = await _service.GetByProyectoIdAsync(proyectoId);
        return Ok(columnas);
    }

    [HttpPost]
    public async Task<ActionResult<ColumnaDto>> Create([FromBody] CreateColumnaDto dto)
    {
        try
        {
            var columna = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = columna.Id }, columna);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateColumnaDto dto)
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