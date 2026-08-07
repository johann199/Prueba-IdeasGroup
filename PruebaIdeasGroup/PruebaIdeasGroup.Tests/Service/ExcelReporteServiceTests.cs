using FluentAssertions;
using PruebaIdeasGroup.Application.Services;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Tests.Helpers;
using Xunit;

namespace PruebaIdeasGroup.Tests.Services;

public class ExcelReporteServiceTests
{
    [Fact]
    public async Task GenerarReporteProyectoExcelAsync_ProyectoNoExiste_LanzaKeyNotFoundException()
    {
        // Arrange
        using var context = TestDbContextFactory.CreateInMemoryContext(Guid.NewGuid().ToString());
        var service = new ExcelReporteService(context);

        // Act & Assert
        await FluentActions.Invoking(() => service.GenerarReporteProyectoExcelAsync(404))
            .Should()
            .ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task GenerarReporteProyectoExcelAsync_ProyectoConColumnasYTareas_GeneraExcelValido()
    {
        // Arrange
        using var context = TestDbContextFactory.CreateInMemoryContext(Guid.NewGuid().ToString());

        // 1. Crear Estado
        var estado = new EstadoProyecto ("Activo");
        context.EstadosProyecto.Add(estado);

        var usuario = new Usuario("Test User", "test@example.com", "hashpass");
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        var proyecto = new Proyecto(
            "Sistema ERP", 
            "Reporte ERP", 
            DateTime.UtcNow, 
            null, 
            usuario.Id, 
            estado.Id
        );
        context.Proyectos.Add(proyecto);
        await context.SaveChangesAsync();

        var columna = new Columna("En Progreso", 1, proyecto.Id);
        context.Columnas.Add(columna);
        await context.SaveChangesAsync();

        var tarea = new Tarea("Desarrollar modulo Excel", "Implementar ClosedXML", 1, 1, columna.Id);
        context.Tareas.Add(tarea);
        await context.SaveChangesAsync();

        var service = new ExcelReporteService(context);
        var resultado = await service.GenerarReporteProyectoExcelAsync(proyecto.Id);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Should().NotBeEmpty();
    }
}