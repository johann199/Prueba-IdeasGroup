namespace PruebaIdeasGroup.Infrastructure.Adapters.Persistence;

using Microsoft.EntityFrameworkCore;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Infrastructure.Data;

public class TareaRepository : ITareaRepository
{
    private readonly ApplicationDbContext _context;

    public TareaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Tarea?> GetByIdAsync(int id)
    {
        return await _context.Tareas
            .Include(t => t.Responsables)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tarea>> GetByColumnaIdAsync(int columnaId)
    {
        return await _context.Tareas
            .Include(t => t.Responsables)
            .Where(t => t.ColumnaId == columnaId)
            .OrderBy(t => t.OrdenDentroColumna)
            .ToListAsync();
    }

    public async Task AddAsync(Tarea tarea)
    {
        await _context.Tareas.AddAsync(tarea);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tarea tarea)
    {
        _context.Tareas.Update(tarea);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tarea = await _context.Tareas.FindAsync(id);
        if (tarea != null)
        {
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
        }
    }
}