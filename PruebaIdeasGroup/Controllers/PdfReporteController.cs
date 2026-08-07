using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaIdeasGroup.Application.Ports.In;

namespace PruebaIdeasGroup.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PdfReporteController : ControllerBase
{
    private readonly IPdfReporteService _pdfReporteService;

    public PdfReporteController(IPdfReporteService pdfReporteService)
    {
        _pdfReporteService = pdfReporteService;
    }

    [HttpGet("proyectos/{proyectoId}/pdf")]
    public async Task<IActionResult> DescargarReporteProyectoPdf(int proyectoId)
    {
        try
        {
            var pdfBytes = await _pdfReporteService.GenerarReporteProyectoPdfAsync(proyectoId);
            var fileName = $"Reporte_Proyecto_{proyectoId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al generar el reporte PDF.", detalle = ex.Message });
        }
    }
}