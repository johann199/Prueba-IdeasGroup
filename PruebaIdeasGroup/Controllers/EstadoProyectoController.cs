using Microsoft.AspNetCore.Mvc;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Application.Ports.In;
namespace PruebaIdeasGroup.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadoProyectoController : ControllerBase
{
    private readonly IEstadoProyectoService _service;

    public EstadoProyectoController(IEstadoProyectoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoProyectoDto>>> GetAll()
    {
        var estados = await _service.GetAllAsync();
        return Ok(estados);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoProyectoDto?>> GetById(int id)
    {
        var estado = await _service.GetByIdAsync(id);
        if (estado is null)
            return NotFound();
        return Ok(estado);
    }

    [HttpPost]
    public async Task<ActionResult<EstadoProyectoDto>> Create(CreateEstadoProyectoDto dto)
    {
        try
        {
            var estado = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = estado.Id }, estado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEstadoProyectoDto dto)
    {
            var updatedEstado = await _service.UpdateAsync(id, dto);
            if (!updatedEstado)
                return NotFound();
            return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedEstado = await _service.DeleteAsync(id);
        if (!deletedEstado)
            return NotFound();
        return NoContent();
    }
}