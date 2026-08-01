namespace PruebaIdeasGroup.Domain.Entities;

public class Tarea
{
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string Descripcion {get; set;} = string.Empty;
    public int OrdenDentroColumna {get; set;}
    public int UsuarioId {get; set;}
    public Usuario ResponsableUsuario {get; set;} = null!;
    public int ColumnaId {get; set;}
    public Columna Columna {get; set;} = null!;
    public DateTime Creado {get; set;}
    public DateTime Modificado {get; set;}

    private Tarea() { }

    public Tarea(string nombre, string descripcion, int ordenDentroColumna, int usuarioId, int columnaId)
    {
        if(string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la tarea no puede estar vacío.", nameof(nombre));
        if(string.IsNullOrWhiteSpace(descripcion))
            throw new ArgumentException("La descripción de la tarea no puede estar vacía.", nameof(descripcion));
        
        Nombre = nombre;
        Descripcion = descripcion;
        OrdenDentroColumna = ordenDentroColumna;
        UsuarioId = usuarioId;
        ColumnaId = columnaId;
        Creado = DateTime.UtcNow;
        Modificado = DateTime.UtcNow;
    }

}