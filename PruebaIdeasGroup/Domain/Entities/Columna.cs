namespace PruebaIdeasGroup.Domain.Entities;

public class Columna
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int OrdenDentroProyecto { get; set; }
    public int ProyectoId { get; set; }
    public Proyecto Proyecto { get; set; } = null!;
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }

    private Columna() { }

    public Columna(string nombre, int ordenDentroProyecto, int proyectoId)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la columna no puede estar vacío.", nameof(nombre));
        
        Nombre = nombre;
        OrdenDentroProyecto = ordenDentroProyecto;
        ProyectoId = proyectoId;
        Creado = DateTime.UtcNow;
        Modificado = DateTime.UtcNow;
    }
}