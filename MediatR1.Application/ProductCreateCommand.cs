using MediatR1.Lib;

namespace MediatR1.Application;

public sealed class ProductCreateCommandRequest : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public sealed class ProductCreateCommandResponse
{
    public string Message { get; set; }
}

internal sealed class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest>
{
    public Task Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}