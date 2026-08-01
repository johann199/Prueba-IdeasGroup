namespace PruebaIdeasGroup.Domain.Entities;

public class Proyecto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public int EstadoId { get; set; }
    public EstadoProyecto Estado { get; set; } = null!;
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }

    private Proyecto() { }

    public Proyecto(string nombre, string descripcion, DateTime fechaInicio, DateTime? fechaFin, int usuarioId, int estadoId)
    {
        if(string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del proyecto no puede estar vacío.", nameof(nombre));
        if(string.IsNullOrWhiteSpace(descripcion))
            throw new ArgumentException("La descripción del proyecto no puede estar vacía.", nameof(descripcion));
        if(fechaFin.HasValue && fechaInicio > fechaFin.Value)
            throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.", nameof(fechaInicio));
        
        Nombre = nombre;
        Descripcion = descripcion;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        UsuarioId = usuarioId;
        EstadoId = estadoId;
        Creado = DateTime.UtcNow;
        Modificado = DateTime.UtcNow;
    }

}