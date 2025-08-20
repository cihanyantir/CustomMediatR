using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR1.Lib;

public static class ServiceRegistrar
{
    public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
    {
       
        var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsInterface && !t.IsAbstract)
            .SelectMany(x=>
                x.GetInterfaces().Where(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IRequestHandler<>))
                    .Select(y => new
                    {
                        Interface = y,
                        Implementation = x
                    })
            );
        foreach (var type in types)
        {
            services.AddTransient(type.Interface, type.Implementation);
        }
        
        services.AddTransient<ISender, Sender>();
        return services;
    }
}