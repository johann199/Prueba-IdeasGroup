namespace PruebaIdeasGroup.Infrastructure.Adapters.Persistence;

using Microsoft.EntityFrameworkCore;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Infrastructure.Data;

public class ProyectoRepository : IProyectoRepository
{
    private readonly ApplicationDbContext _context;

    public ProyectoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Proyecto?> GetByIdAsync(int id)
    {
        return await _context.Proyectos
            .Include(p => p.CreadoPor)
            .Include(p => p.Estado)
            .Include(p => p.Equipo)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Proyecto>> GetAllAsync()
    {
        return await _context.Proyectos
            .Include(p => p.CreadoPor)
            .Include(p => p.Estado)
            .ToListAsync();
    }

    public async Task AddAsync(Proyecto proyecto)
    {
        await _context.Proyectos.AddAsync(proyecto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Proyecto proyecto)
    {
        _context.Proyectos.Update(proyecto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var proyecto = await _context.Proyectos.FindAsync(id);
        if (proyecto != null)
        {
            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();
        }
    }
}