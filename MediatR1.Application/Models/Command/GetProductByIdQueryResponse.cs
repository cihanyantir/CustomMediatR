using MediatR1.Lib.Interfaces;

namespace MediatR1.Application.Models.Command;

public sealed class GetProductByIdQueryResponse : IResponse
{
    public string Name { get; set; }
}
