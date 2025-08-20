using MediatR1.Application;
using MediatR1.Lib;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
var app = builder.Build();
// Add MediatR services


app.MapGet("/", () => "Hello World!");

app.MapPost("/try",async (ProductCreateCommandRequest request, ISender sender) =>
{
  await sender.Send<ProductCreateCommandRequest>(request, CancellationToken.None);
});

app.Run();