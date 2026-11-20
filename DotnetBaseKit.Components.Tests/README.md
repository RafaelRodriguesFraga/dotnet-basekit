# DotnetBaseKit.Components.Tests Documentation

Este projeto contém os testes unitários e mocks usados para validar os componentes do DotnetBaseKit.

<details open>
  
  <summary>Table of Contents</summary>

- [Overview](#overview)
- [Run Tests](#run-tests)
- [Structure](#structure)
- [Guidelines](#guidelines)

</details>

## Overview

O projeto `DotnetBaseKit.Components.Tests` agrupa testes de unidade e testes de integração leve (quando necessário) para os pacotes do DotnetBaseKit. Ele fornece exemplos de uso dos componentes e valida comportamentos centrais (Responses, Notifications, Pagination, etc.).

## Run Tests

Execute os testes com o .NET CLI a partir da raiz do workspace ou da pasta do projeto:

```bash
dotnet test DotnetBaseKit.Components.Tests\DotnetBaseKit.Components.Tests.csproj
```

Ou, para rodar todos os testes da solução:

```bash
dotnet test
```

## Structure

- `Unit/` - testes unitários (xUnit / MSTest / NUnit conforme o projeto).
- `Mocks/` - implementações de teste e stubs para dependências.

## Guidelines

- Mantenha os testes pequenos e determinísticos.
- Use os mocks/fakes em `Mocks/` para isolar dependências externas.
- Se precisar de um ambiente integrado (BD, Redis, etc.), marque o teste explicitamente e documente dependências no próprio teste.

---

Contribuições e melhorias aos testes são bem-vindas. Veja o README da solução principal para instruções de contribuição.
