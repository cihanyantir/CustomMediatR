using MediatR1.Application.Models.Command;
using MediatR1.Lib.Interfaces;

namespace MediatR1.Application;
internal sealed class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, ProductCreateCommandResponse>
{
    public async Task<ProductCreateCommandResponse> Handle(ProductCreateCommandRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        
        return new ProductCreateCommandResponse
        {
            Message = $"Product with ID {request.Id} and Name '{request.Name}' created successfully."
        };
    }
}

internal sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
{
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);
        
        return new GetProductByIdQueryResponse
        {
            Name = $"Product Name for ID {request.Id}"
        };
    }
}