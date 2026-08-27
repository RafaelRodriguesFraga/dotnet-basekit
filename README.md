# DotnetBaseKit

O **DotnetBaseKit** é um acelerador de desenvolvimento em .NET projetado para simplificar e padronizar a criação de APIs, microsserviços e aplicações baseadas em **Domain-Driven Design (DDD)**. Ele abstrai padrões recorrentes como controle de respostas HTTP, padrão Notification, manipulação de repositórios leitura/escrita e suporte a MongoDB e bancos relacionais (SQL / Entity Framework Core).

---

## 📦 Arquitetura dos Componentes

O projeto é estruturado em módulos modulares e reutilizáveis:

| Módulo                                        | Descrição                                                                            |
| :-------------------------------------------- | :----------------------------------------------------------------------------------- |
| **`DotnetBaseKit.Components.Shared`**         | Notificações de domínio, formatadores e o padrão `Notifiable`.                       |
| **`DotnetBaseKit.Components.Domain.Sql`**     | Interfaces base para entidades, DTOs e repositórios SQL.                             |
| **`DotnetBaseKit.Components.Domain.MongoDb`** | Interfaces e classes base para entidades e repositórios MongoDB.                     |
| **`DotnetBaseKit.Components.Infra.Sql`**      | Implementações genéricas de Contexto EF Core e repositórios SQL.                     |
| **`DotnetBaseKit.Components.Infra.MongoDb`**  | Configuração de sessões, conexões e repositórios genéricos MongoDB.                  |
| **`DotnetBaseKit.Components.Application`**    | Serviços de aplicação base, abstrações de paginação e mapeamentos.                   |
| **`DotnetBaseKit.Components.Api`**            | Controllers base (`ApiControllerBase`) e respostas padronizadas (`ResponseFactory`). |

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

- **.NET SDK 8.0** ou superior
- **MongoDB** _(opcional para testes locais com NoSQL)_
- **SQL Server / PostgreSQL** _(opcional para testes locais com banco relacional)_

### Executando os Testes de Unidade

O projeto possui uma suíte completa de testes de unidade para garantir a confiabilidade das abstrações.

```bash
# Executando via script utilitário
./scripts/tests.sh

# Ou diretamente via dotnet
dotnet test DotnetBaseKit.Components.Tests/DotnetBaseKit.Components.Tests.csproj
```

### Executando o Playground (`TestApi`)

Para visualizar o uso prático de todas as camadas e componentes em conjunto, utilize a aplicação de testes contida na pasta `Playground`:

```bash
cd Playground/TestApi.Api
dotnet run
```

---

## 💻 Exemplo de Uso Prático

### 1. Configurando a Controller na Camada API

A classe `ApiControllerBase` gerencia automaticamente a conversão de notificações do serviço em respostas HTTP adequadas (ex.: retornando `400 Bad Request` em caso de erros de validação ou `200 OK` em caso de sucesso).

```csharp
using DotnetBaseKit.Components.Api.Base;
using DotnetBaseKit.Components.Api.Responses;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestApiController : ApiControllerBase
{
    private readonly ITestApiServiceApplication _service;

    public TestApiController(
        IResponseFactory responseFactory,
        ITestApiServiceApplication service) : base(responseFactory)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TestApiViewModel viewModel)
    {
        var result = await _service.AddAsync(viewModel);
        return CustomResponse(result); // Trata o status HTTP e notificações automaticamente
    }
}
```

### 2. Implementando o Serviço de Aplicação

A classe `BaseServiceApplication` injeta o contexto de notificações e gerencia fluxos operacionais básicos.

```csharp
using DotnetBaseKit.Components.Application.Base;
using DotnetBaseKit.Components.Shared.Notifications;

public class TestApiServiceApplication : BaseServiceApplication, ITestApiServiceApplication
{
    private readonly ITestApiWriteRepository _repository;

    public TestApiServiceApplication(
        INotificationContext notificationContext,
        ITestApiWriteRepository repository) : base(notificationContext)
    {
        _repository = repository;
    }

    public async Task<bool> AddAsync(TestApiViewModel viewModel)
    {
        var entity = new Test(viewModel.Name);

        // Se a entidade for inválida, as notificações são injetadas no NotificationContext
        if (!entity.IsValid())
        {
            AddNotifications(entity.ValidationResult);
            return false;
        }

        return await _repository.AddAsync(entity);
    }
}
```

---

## 📄 CI/CD & Publicação

O repositório possui fluxos automatizados via **GitHub Actions**:

- Execução contínua de testes em Pull Requests.
- Publicação automática de pacotes NuGet via release semântica (`.github/workflows/release.yaml`).
