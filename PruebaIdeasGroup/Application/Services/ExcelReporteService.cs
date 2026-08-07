using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Infrastructure.Data;

namespace PruebaIdeasGroup.Application.Services;

public class ExcelReporteService : IExcelReporteService
{
    private readonly ApplicationDbContext _context;

    public ExcelReporteService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> GenerarReporteProyectoExcelAsync(int proyectoId)
    {
        
        var proyecto = await _context.Proyectos
            .Include(p => p.Estado)
            .FirstOrDefaultAsync(p => p.Id == proyectoId);

        if (proyecto == null)
        {
            throw new KeyNotFoundException($"No se encontró el proyecto con ID {proyectoId}");
        }

        
        var columnas = await _context.Columnas
            .Where(c => c.ProyectoId == proyectoId)
            .OrderBy(c => c.OrdenDentroProyecto)
            .ToListAsync();

        var columnasIds = columnas.Select(c => c.Id).ToList();

        
        var tareas = await _context.Tareas
            .Where(t => columnasIds.Contains(t.ColumnaId))
            .OrderBy(t => t.OrdenDentroColumna)
            .ToListAsync();

        
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Reporte de Proyecto");

        
        worksheet.Cell("A1").Value = $"REPORTE DE PROYECTO: {proyecto.Nombre.ToUpper()}";
        worksheet.Cell("A1").Style.Font.Bold = true;
        worksheet.Cell("A1").Style.Font.FontSize = 14;

        worksheet.Cell("A2").Value = $"Estado: {proyecto.Estado?.Nombre ?? "Sin Estado"}";
        worksheet.Cell("A2").Style.Font.Italic = true;

        worksheet.Cell("A3").Value = $"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}";
        worksheet.Cell("A3").Style.Font.FontSize = 9;

        if (!string.IsNullOrWhiteSpace(proyecto.Descripcion))
        {
            worksheet.Cell("A4").Value = $"Descripción: {proyecto.Descripcion}";
            worksheet.Cell("A4").Style.Font.FontSize = 10;
        }

        
        int filaInicioHeader = 6;

        
        worksheet.Cell(filaInicioHeader, 1).Value = "ID Tarea";
        worksheet.Cell(filaInicioHeader, 2).Value = "Columna";
        worksheet.Cell(filaInicioHeader, 3).Value = "Nombre Tarea";
        worksheet.Cell(filaInicioHeader, 4).Value = "Descripción";
        worksheet.Cell(filaInicioHeader, 5).Value = "Prioridad";
        worksheet.Cell(filaInicioHeader, 6).Value = "Fecha Creación";

       
        var headerRange = worksheet.Range(filaInicioHeader, 1, filaInicioHeader, 6);
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
        headerRange.Style.Font.FontColor = XLColor.White;
        headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        int filaActual = filaInicioHeader + 1;

        foreach (var columna in columnas)
        {
            var tareasDeColumna = tareas.Where(t => t.ColumnaId == columna.Id).ToList();

            if (!tareasDeColumna.Any())
            {
                worksheet.Cell(filaActual, 1).Value = "-";
                worksheet.Cell(filaActual, 2).Value = columna.Nombre;
                worksheet.Cell(filaActual, 3).Value = "(Sin tareas)";
                worksheet.Cell(filaActual, 3).Style.Font.Italic = true;
                worksheet.Cell(filaActual, 4).Value = "-";
                worksheet.Cell(filaActual, 5).Value = "-";
                worksheet.Cell(filaActual, 6).Value = "-";
                filaActual++;
            }
            else
            {
                foreach (var tarea in tareasDeColumna)
                {
                    worksheet.Cell(filaActual, 1).Value = tarea.Id;
                    worksheet.Cell(filaActual, 2).Value = columna.Nombre;
                    worksheet.Cell(filaActual, 3).Value = tarea.Nombre;
                    worksheet.Cell(filaActual, 4).Value = tarea.Descripcion ?? "";
                    worksheet.Cell(filaActual, 5).Value = tarea.Prioridad;
                    worksheet.Cell(filaActual, 6).Value = tarea.Creado.ToString("dd/MM/yyyy HH:mm");

                    filaActual++;
                }
            }
        }

       
        var dataRange = worksheet.Range(filaInicioHeader, 1, filaActual - 1, 6);
        dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}