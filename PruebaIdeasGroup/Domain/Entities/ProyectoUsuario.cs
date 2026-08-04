namespace PruebaIdeasGroup.Domain.Entities;

public class ProyectoUsuario
{
    public int Id {get; set;}
    public int ProyectoId {get; set;}
    public  Proyecto Proyecto {get; set;}= null!;
    public int UsuarioId {get; set;}
    public Usuario Usuario {get; set;}=null!;
    public DateTime FechaAsignacion {get; set;} = DateTime.UtcNow;

    public ProyectoUsuario() {}

    public ProyectoUsuario(int proyectoId, int usuarioId)
    {
        ProyectoId = proyectoId;
        UsuarioId = usuarioId;
        FechaAsignacion = DateTime.UtcNow;
    }
}