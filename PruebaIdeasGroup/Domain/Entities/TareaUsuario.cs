namespace PruebaIdeasGroup.Domain.Entities;

public class TareaUsuario
{
    public int Id {get; set;}
    public int TareaId {get; set;} 
    public Tarea Tarea {get; set;} = null!;
    public int UsuarioId {get; set;} 
    public Usuario Usuario {get; set;} = null!;
    public DateTime FechaAsignacion {get; set;} = DateTime.UtcNow;
    private TareaUsuario(){}
    public TareaUsuario(int tareaId, int usuarioId)
    {
        TareaId = tareaId;
        UsuarioId = usuarioId;
        FechaAsignacion = DateTime.UtcNow;
    } 

}