# MediatR1 - Custom MediatR Implementation

Bu proje, .NET 8 kullanarak MediatR pattern'ini sıfırdan implement eden bir örnek projedir. CQRS (Command Query Responsibility Segregation) pattern'ini kullanarak, command ve query'leri ayrı handler'lar ile işleyen bir yapı sunar.

## 🚀 Özellikler

- **Custom MediatR Implementation**: MediatR kütüphanesini kullanmadan, kendi implementasyonunuzu oluşturun
- **CQRS Pattern**: Command ve Query'leri ayrı handler'lar ile işleyin
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection ile entegre
- **Async/Await Support**: Tüm operasyonlar asenkron olarak çalışır
- **Clean Architecture**: Katmanlı mimari ile düzenli kod yapısı

## 🏗️ Proje Yapısı
MediatR1/
├── MediatR1.API/ # Web API katmanı
├── MediatR1.Application/ # Uygulama katmanı (Command/Query handlers)
└── MediatR1.Lib/ # Core kütüphane (Interfaces ve implementations)


### Katmanlar

- **MediatR1.API**: REST API endpoints ve program konfigürasyonu
- **MediatR1.Application**: Business logic ve command/query handler'ları
- **MediatR1.Lib**: Core interfaces, Sender implementation ve service registration

## 🛠️ Teknolojiler

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Microsoft.Extensions.DependencyInjection**

## 📦 Kurulum

### Gereksinimler

- .NET 8.0 SDK
- Visual Studio 2022 veya VS Code

### Adımlar

1. Projeyi klonlayın:
```bash
git clone https://github.com/yourusername/MediatR1.git
cd MediatR1
```

2. Projeyi çalıştırın:
```bash
dotnet run --project MediatR1.API
```

3. Tarayıcınızda `https://localhost:5001` adresini açın

## 🔧 Kullanım

### API Endpoints

#### Product Oluşturma
```http
POST /try
Content-Type: application/json

{
  "id": 1,
  "name": "Test Product"
}
```

#### Product ID ile Sorgulama
```http
POST /getId
Content-Type: application/json

{
  "id": 1
}
```

### Handler Ekleme

Yeni bir command handler eklemek için:

```csharp
public class CreateUserCommandRequest : IRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
}

public class CreateUserCommandResponse : IResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        // Business logic burada
        return new CreateUserCommandResponse
        {
            Success = true,
            Message = $"User {request.Username} created successfully"
        };
    }
}
```

## 🏗️ Mimari

### Core Interfaces

- **IRequest**: Tüm request'lerin implement ettiği base interface
- **IResponse**: Tüm response'ların implement ettiği base interface
- **IRequestHandler<TRequest>**: Command handler'lar için interface
- **IRequestHandler<TRequest, TResponse>**: Query handler'lar için interface
- **ISender**: Request'leri dispatch eden interface

### Sender Implementation

`Sender` sınıfı, reflection kullanarak request'leri uygun handler'lara yönlendirir:

```csharp
public sealed class Sender : ISender
{
    public async Task<TResponse> Send<TRequest, TResponse>(IRequest request, CancellationToken cancellationToken = default)
    {
        var type = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService(type);
        return await (Task<TResponse>) type.GetMethod("Handle")?.Invoke(handler, [request, cancellationToken])!;
    }
}
```

## 🔄 Service Registration

Handler'lar otomatik olarak tespit edilir ve DI container'a kaydedilir:

```csharp
public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
{
    var types = assembly.GetTypes()
        .Where(t => t.IsClass && !t.IsInterface && !t.IsAbstract)
        .SelectMany(x => x.GetInterfaces()
            .Where(y => y.IsGenericType && 
                   (y.GetGenericTypeDefinition() == typeof(IRequestHandler<>) || 
                    y.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .Select(y => new { Interface = y, Implementation = x }));
    
    foreach (var type in types)
    {
        services.AddTransient(type.Interface, type.Implementation);
    }
    
    services.AddTransient<ISender, Sender>();
    return services;
}
```

## 📝 Örnek Kullanım

```csharp
// Program.cs
app.MapPost("/try", async (ProductCreateCommandRequest request, ISender sender) =>
{
    var response = await sender.Send<ProductCreateCommandRequest, ProductCreateCommandResponse>(request, CancellationToken.None);
    return response;
});
```

