using MediatR1.Application;
using MediatR1.Application.Models.Command;
using MediatR1.Lib.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/try",async (ProductCreateCommandRequest request, ISender sender) =>
{
  await sender.Send<ProductCreateCommandRequest,ProductCreateCommandResponse>(request, CancellationToken.None);
});

app.MapPost("/getId",async (GetProductByIdQueryRequest request, ISender sender) =>
{
  await sender.Send<GetProductByIdQueryRequest,GetProductByIdQueryResponse>(request, CancellationToken.None);
});

app.Run();