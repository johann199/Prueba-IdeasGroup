using Microsoft.EntityFrameworkCore;
using PruebaIdeasGroup.Domain.Entities;

namespace PruebaIdeasGroup.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Proyecto> Proyectos => Set<Proyecto>();
    public DbSet<Columna> Columnas => Set<Columna>();
    public DbSet<Tarea> Tareas => Set<Tarea>();
    public DbSet<EstadoProyecto> EstadosProyecto => Set<EstadoProyecto>();
}