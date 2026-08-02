using Microsoft.EntityFrameworkCore;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Domain.Ports;
using PruebaIdeasGroup.Infrastructure.Data;

namespace PruebaIdeasGroup.Infrastructure.Repository;


public class EstadoProyectoRepository : IEstadoProyectoRepository
{
    private readonly ApplicationDbContext _context;
    public EstadoProyectoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EstadoProyecto?> GetByIdAsync(int id)
    {
        return await _context.EstadosProyecto.FindAsync(id);
    }

    public async Task<IEnumerable<EstadoProyecto>> GetAllAsync()
    {
        return await _context.EstadosProyecto.ToListAsync();
    }

    public async Task AddAsync(EstadoProyecto estadoProyecto)
    {
        await _context.EstadosProyecto.AddAsync(estadoProyecto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EstadoProyecto estadoProyecto)
    {
        _context.EstadosProyecto.Update(estadoProyecto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var estadoProyecto = await _context.EstadosProyecto.FindAsync(id);
        if (estadoProyecto != null)
        {
            _context.EstadosProyecto.Remove(estadoProyecto);
            await _context.SaveChangesAsync();
        }
    }
}

