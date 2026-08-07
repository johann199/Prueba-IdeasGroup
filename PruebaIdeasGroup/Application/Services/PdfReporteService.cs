using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Infrastructure.Data;

namespace PruebaIdeasGroup.Application.Services;

public class PdfReporteService : IPdfReporteService
{
    private readonly ApplicationDbContext _context;

    public PdfReporteService(ApplicationDbContext context)
    {
        _context = context;
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<byte[]> GenerarReporteProyectoPdfAsync(int proyectoId)
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

    
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text($"REPORTE DE PROYECTO: {proyecto.Nombre.ToUpper()}")
                            .Bold().FontSize(16).FontColor(Colors.Blue.Darken2);
                        col.Item().Text($"Estado: {proyecto.Estado?.Nombre ?? "Sin Estado"}")
                            .FontSize(10).Italic().FontColor(Colors.Grey.Darken1);
                        col.Item().Text($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(9).FontColor(Colors.Grey.Medium);
                    });
                });

            
                page.Content().PaddingVertical(1, Unit.Centimetre).Column(col =>
                {
                    if (!string.IsNullOrWhiteSpace(proyecto.Descripcion))
                    {
                        col.Item().PaddingBottom(10).Text($"Descripción: {proyecto.Descripcion}")
                            .FontSize(10);
                    }

                    col.Item().PaddingBottom(5).Text("Estructura del Tablero Kanban")
                        .Bold().FontSize(12).FontColor(Colors.Grey.Darken3);

                    
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columnsDef =>
                        {
                            columnsDef.ConstantColumn(40); 
                            columnsDef.RelativeColumn(2);   
                            columnsDef.RelativeColumn(4);   
                            columnsDef.ConstantColumn(60);  
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("ID").Bold();
                            header.Cell().Element(CellStyle).Text("Columna").Bold();
                            header.Cell().Element(CellStyle).Text("Tarea").Bold();
                            header.Cell().Element(CellStyle).Text("Prioridad").Bold();

                            static IContainer CellStyle(IContainer container) =>
                                container.Background(Colors.Grey.Lighten2)
                                         .Padding(5)
                                         .BorderBottom(1)
                                         .BorderColor(Colors.Grey.Darken1);
                        });

                        int contador = 1;

                        foreach (var columna in columnas)
                        {
                            
                            var tareasDeColumna = tareas.Where(t => t.ColumnaId == columna.Id).ToList();

                            if (!tareasDeColumna.Any())
                            {
                                table.Cell().Element(RowStyle).Text(contador++.ToString());
                                table.Cell().Element(RowStyle).Text(columna.Nombre);
                                table.Cell().Element(RowStyle).Text("(Sin tareas)").Italic().FontColor(Colors.Grey.Medium);
                                table.Cell().Element(RowStyle).Text("-");
                            }
                            else
                            {
                                foreach (var tarea in tareasDeColumna)
                                {
                                    table.Cell().Element(RowStyle).Text(contador++.ToString());
                                    table.Cell().Element(RowStyle).Text(columna.Nombre);
                                    table.Cell().Element(RowStyle).Text(tarea.Nombre);
                                    table.Cell().Element(RowStyle).Text(tarea.Prioridad.ToString());
                                }
                            }
                        }

                        static IContainer RowStyle(IContainer container) =>
                            container.BorderBottom(0.5f)
                                     .BorderColor(Colors.Grey.Lighten1)
                                     .Padding(5);
                    });
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Página ");
                    x.CurrentPageNumber();
                    x.Span(" de ");
                    x.TotalPages();
                });
            });
        });

        return document.GeneratePdf();
    }
}