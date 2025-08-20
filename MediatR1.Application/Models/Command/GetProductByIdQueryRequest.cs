using MediatR1.Lib.Interfaces;

namespace MediatR1.Application.Models.Command;

public sealed class GetProductByIdQueryRequest : IRequest
{
    public int Id { get; set; }
}