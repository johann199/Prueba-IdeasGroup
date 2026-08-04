namespace PruebaIdeasGroup.Application.Dtos;

public class ProyectoDto
{
    public int Id {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string Descripcion {get; set;} = string.Empty;
    public int UsuarioId {get; set;}
    public UsuarioDto? Usuario {get; set;}
    public int EstadoId {get; set;}
    public EstadoProyectoDto? Estado {get; set;}
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime Creado {get; set;}
    public DateTime Modificado {get; set;}
}

public class CreateProyectoDto
{
    public string Nombre {get; set;} = string.Empty;
    public string Descripcion {get; set;} = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int UsuarioId {get; set;}
    public int EstadoId {get; set;}
}


public class UpdateProyectoDto
{
    public string Nombre {get; set;} = string.Empty;
    public string Descripcion {get; set;} = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public int UsuarioId {get; set;}
    public int EstadoId {get; set;}
    
}