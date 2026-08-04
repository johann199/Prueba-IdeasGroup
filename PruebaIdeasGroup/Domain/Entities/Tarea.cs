namespace PruebaIdeasGroup.Domain.Entities;

public class Tarea
{
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string Descripcion {get; set;} = string.Empty;
    public int OrdenDentroColumna {get; set;}
    public int Prioridad {get; set;}
    public int ColumnaId {get; set;}
    public Columna Columna {get; set;} = null!;
    private readonly List<TareaUsuario> _responsables = new();
    public IReadOnlyCollection<TareaUsuario> Responsables => _responsables.AsReadOnly();
    public DateTime Creado {get; set;}
    public DateTime Modificado {get; set;}

    private Tarea() { }

    public Tarea(string nombre, string descripcion, int prioridad, int ordenDentroColumna,  int columnaId)
    {
        if(string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la tarea no puede estar vacío.", nameof(nombre));
        if(string.IsNullOrWhiteSpace(descripcion))
            throw new ArgumentException("La descripción de la tarea no puede estar vacía.", nameof(descripcion));
        
        Nombre = nombre;
        Descripcion = descripcion;
        Prioridad = prioridad;
        OrdenDentroColumna = ordenDentroColumna;
        ColumnaId = columnaId;
        Creado = DateTime.UtcNow;
        Modificado = DateTime.UtcNow;
    }

    public void AddResponsableTarea(int usuarioId)
    {
        if (!_responsables.Any(r=> r.UsuarioId == usuarioId))
        {
            _responsables.Add(new TareaUsuario(Id, usuarioId));
            Modificado = DateTime.UtcNow;
        }
    }

}