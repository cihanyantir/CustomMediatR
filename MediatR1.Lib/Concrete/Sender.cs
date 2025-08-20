using MediatR1.Lib.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR1.Lib.Concrete;

public sealed class Sender(IServiceProvider serviceProvider) : ISender
{
    public async Task Send<TRequest>(IRequest request, CancellationToken cancellationToken = default)
    {
        var type = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService(type);
        await (Task) type.GetMethod("Handle")?.Invoke(handler, [request, cancellationToken])!;
    }

    public async Task<TResponse> Send<TRequest, TResponse>(IRequest request, CancellationToken cancellationToken = default) where TResponse : IResponse
    {
        var type = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService(type);
        return await (Task<TResponse>) type.GetMethod("Handle")?.Invoke(handler, [request, cancellationToken])!;
    }
}