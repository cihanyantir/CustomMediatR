using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
namespace MediatR1.Lib;


public interface IRequest { }

public interface IRequestHandler <in TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}
public interface ISender
{
    Task Send<TRequest>(IRequest request, CancellationToken cancellationToken = default);
}

public sealed class Sender(IServiceProvider serviceProvider) : ISender
{
    public async Task Send<TRequest>(IRequest request, CancellationToken cancellationToken = default)
    {
        var type = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService(type);
        await (Task) type.GetMethod("Handle")?.Invoke(handler, [request, cancellationToken])!;
    }
}