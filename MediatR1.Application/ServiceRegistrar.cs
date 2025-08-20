using MediatR1.Lib;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR1.Application;

public static class ServiceRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceRegistrar).Assembly);
        return services;
    }
}