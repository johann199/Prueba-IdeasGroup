using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaIdeasGroup.Application.Ports.In;

namespace PruebaIdeasGroup.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExcelReporteController : ControllerBase
{
    private readonly IExcelReporteService _excelReporteService;

    public ExcelReporteController(IExcelReporteService excelReporteService)
    {
        _excelReporteService = excelReporteService;
    }

    [HttpGet("proyectos/{proyectoId}/excel")]
    public async Task<IActionResult> DescargarReporteProyectoExcel(int proyectoId)
    {
        try
        {
            var excelBytes = await _excelReporteService.GenerarReporteProyectoExcelAsync(proyectoId);
            var fileName = $"Reporte_Proyecto_{proyectoId}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al generar el reporte en Excel.", detalle = ex.Message });
        }
    }
}