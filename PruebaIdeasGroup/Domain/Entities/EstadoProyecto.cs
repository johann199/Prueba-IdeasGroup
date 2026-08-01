namespace PruebaIdeasGroup.Domain.Entities;

public class EstadoProyecto
{
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public DateTime Creado {get; set;}
    public DateTime Modificado {get; set;}

    private EstadoProyecto(){}

    public EstadoProyecto(string nombre)
    {
        if(string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del estado del proyecto no puede estar vacío.", nameof(nombre));
        Nombre = nombre;
        Creado = DateTime.UtcNow;
        Modificado = DateTime.UtcNow;
    }
}