namespace MediatR1.Lib.Interfaces;

public interface IRequest { }

public interface IResponse { }

public interface IRequestHandler <in TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, CancellationToken cancellationToken);
}

public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest where TResponse : IResponse
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}

public interface ISender
{
    Task Send<TRequest>(IRequest request, CancellationToken cancellationToken = default);
    Task<TResponse> Send<TRequest, TResponse>(IRequest request, CancellationToken cancellationToken = default) where TResponse : IResponse;
}