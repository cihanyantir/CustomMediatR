using MediatR1.Lib.Interfaces;

namespace MediatR1.Application.Models.Command;

public sealed class ProductCreateCommandResponse : IResponse
{
    public string Message { get; set; }
}
