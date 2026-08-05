namespace PruebaIdeasGroup.Application.Dtos;

public class ColumnaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int OrdenDentroProyecto { get; set; }
    public int ProyectoId { get; set; }
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }
}

public class CreateColumnaDto
{
    public string Nombre { get; set; } = string.Empty;
    public int OrdenDentroProyecto { get; set; }
    public int ProyectoId { get; set; }
}

public class UpdateColumnaDto
{
    public string Nombre { get; set; } = string.Empty;
    public int OrdenDentroProyecto { get; set; }
}