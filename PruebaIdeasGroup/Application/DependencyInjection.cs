namespace PruebaIdeasGroup.Application;

using Microsoft.Extensions.DependencyInjection;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IEstadoProyectoService, EstadoProyectoService>();
        services.AddScoped<IProyectoService, ProyectoService>();
        services.AddScoped<IColumnaService, ColumnaService>();
        services.AddScoped<ITareaService, TareaService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<INotificacionService, SignalRNotificacionService>();
        services.AddScoped<IPdfReporteService, PdfReporteService>();
        services.AddScoped<IExcelReporteService, ExcelReporteService>();

        return services;
    }
}