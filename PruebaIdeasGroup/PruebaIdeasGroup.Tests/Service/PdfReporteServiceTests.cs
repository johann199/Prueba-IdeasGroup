using FluentAssertions;
using PruebaIdeasGroup.Application.Services;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Tests.Helpers;
using Xunit;

namespace PruebaIdeasGroup.Tests.Services;

public class PdfReporteServiceTests
{
    [Fact]
    public async Task GenerarReporteProyectoPdfAsync_ProyectoNoExiste_LanzaKeyNotFoundException()
    {
        // Arrange
        using var context = TestDbContextFactory.CreateInMemoryContext(Guid.NewGuid().ToString());
        var service = new PdfReporteService(context);

        // Act & Assert
        await FluentActions.Invoking(() => service.GenerarReporteProyectoPdfAsync(99))
            .Should()
            .ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async Task GenerarReporteProyectoPdfAsync_ProyectoExiste_RetornaByteArray()
    {
        // Arrange
        using var context = TestDbContextFactory.CreateInMemoryContext(Guid.NewGuid().ToString());

        // 1. Crear Estado
        var estado = new EstadoProyecto ("Activo");
        context.EstadosProyecto.Add(estado);

        // 2. Crear Usuario Creador
        var usuario = new Usuario("Test User", "test@example.com", "hashpass");
        context.Usuarios.Add(usuario);
        await context.SaveChangesAsync();

        // 3. Crear Proyecto
        var proyecto = new Proyecto(
            "Proyecto Test", 
            "Descripción Test", 
            DateTime.UtcNow, 
            null, 
            usuario.Id, 
            estado.Id
        );
        context.Proyectos.Add(proyecto);
        await context.SaveChangesAsync();

        // 4. Crear Columna y Tarea
        var columna = new Columna("To Do", 1, proyecto.Id);
        context.Columnas.Add(columna);
        await context.SaveChangesAsync();

        var tarea = new Tarea("Tarea 1", "Descripción Tarea", 1, 1, columna.Id);
        context.Tareas.Add(tarea);
        await context.SaveChangesAsync();

        // Act
        var service = new PdfReporteService(context);
        var resultado = await service.GenerarReporteProyectoPdfAsync(proyecto.Id);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Length.Should().BeGreaterThan(0);
    }
}