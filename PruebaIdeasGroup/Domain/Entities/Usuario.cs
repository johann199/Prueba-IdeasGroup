namespace PruebaIdeasGroup.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public DateTime Creado { get; set; }
    public DateTime Modificado { get; set; }
    private Usuario() { }
    public Usuario(string nombre, string correo, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del usuario no puede estar vacío.", nameof(nombre));
        if (string.IsNullOrWhiteSpace(correo))
            throw new ArgumentException("El correo del usuario no puede estar vacío.", nameof(correo));
        if (string.IsNullOrWhiteSpace(contrasena))
            throw new ArgumentException("La contraseña del usuario no puede estar vacía.", nameof(contrasena));

        Nombre = nombre;
        Correo = correo;
        Contrasena = contrasena;
        Creado = DateTime.UtcNow;
        Modificado = DateTime.UtcNow;
    }

}