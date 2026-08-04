namespace PruebaIdeasGroup.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Infrastructure.Adapters.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IEstadoProyectoRepository, EstadoProyectoRepository>();
        services.AddScoped<IProyectoRepository, ProyectoRepository>();
        // services.AddScoped<IColumnaRepository, ColumnaRepository>();
        // services.AddScoped<ITareaRepository, TareaRepository>();

        return services;
    }
}