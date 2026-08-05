namespace PruebaIdeasGroup.Application.Dtos;

public class TareaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int OrdenDentroColumna { get; set; }
    public int Prioridad { get; set; }
    public int ColumnaId { get; set; }
    public List<int> ResponsablesIds { get; set; } = new();
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }
}

public class CreateTareaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Prioridad { get; set; }
    public int OrdenDentroColumna { get; set; }
    public int ColumnaId { get; set; }
}

public class UpdateTareaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Prioridad { get; set; }
    public int OrdenDentroColumna { get; set; }
    public int ColumnaId { get; set; }
}

public class AddResponsableDto
{
    public int UsuarioId { get; set; }
}
