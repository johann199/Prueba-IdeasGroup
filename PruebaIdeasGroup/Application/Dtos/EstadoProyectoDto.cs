namespace PruebaIdeasGroup.Application.Dtos;

public class EstadoProyectoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }
}

public class CreateEstadoProyectoDto
{
    public string Nombre { get; set; } = string.Empty;
}

public class UpdateEstadoProyectoDto
{
    public string Nombre { get; set; } = string.Empty;
}

