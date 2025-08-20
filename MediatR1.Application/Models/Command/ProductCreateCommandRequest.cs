using MediatR1.Lib;
using MediatR1.Lib.Interfaces;

namespace MediatR1.Application.Models.Command;

public sealed class ProductCreateCommandRequest : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
}
