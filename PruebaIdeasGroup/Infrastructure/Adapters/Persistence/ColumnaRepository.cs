namespace PruebaIdeasGroup.Infrastructure.Adapters.Persistence;

using Microsoft.EntityFrameworkCore;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Infrastructure.Data;

public class ColumnaRepository : IColumnaRepository
{
    private readonly ApplicationDbContext _context;

    public ColumnaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Columna?> GetByIdAsync(int id)
    {
        return await _context.Columnas
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Columna>> GetByProyectoIdAsync(int proyectoId)
    {
        return await _context.Columnas
            .Where(c => c.ProyectoId == proyectoId)
            .OrderBy(c => c.OrdenDentroProyecto)
            .ToListAsync();
    }

    public async Task AddAsync(Columna columna)
    {
        await _context.Columnas.AddAsync(columna);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Columna columna)
    {
        _context.Columnas.Update(columna);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var columna = await _context.Columnas.FindAsync(id);
        if (columna != null)
        {
            _context.Columnas.Remove(columna);
            await _context.SaveChangesAsync();
        }
    }
}