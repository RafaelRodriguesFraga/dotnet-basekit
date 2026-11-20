# DotnetBaseKit.Components.Infra.Sql Documentation

Implementations and DI registration helpers for relational databases used by the DotnetBaseKit examples. This package exposes an extension to register an EF Core `DbContext` configured for multiple providers, and it registers generic SQL repositories.

<details open>
  
  <summary>Table of Contents</summary>

- [DotnetBaseKit.Components.Infra.Sql Documentation](#dotnetbasekitcomponentsinfrasql-documentation)
  - [Overview](#overview)
  - [Supported Providers](#supported-providers)
  - [Configuration (appsettings.json)](#configuration-appsettingsjson)
  - [How to register](#how-to-register)
  - [What is registered](#what-is-registered)
  - [Notes](#notes)
  - [Examples and links](#examples-and-links)

</details>

## Overview

`DotnetBaseKit.Components.Infra.Sql` simplifies wiring a relational `DbContext` and repository implementations. The main helper is `RepositoryExtensions.AddDbContext<TContext>(IServiceCollection, IConfiguration)` which chooses the provider at runtime based on a `SelectedDatabase` configuration value.

## Supported Providers

- `MySql` (uses `UseMySql` and `ServerVersion.AutoDetect`)
- `SqlServer` (uses `UseSqlServer`)
- `Postgres` (uses `UseNpgsql`)
- `SqLite` (uses `UseSqlite`)

The provider name is case-sensitive and must match one of the values above (note the `SqLite` spelling used in the extension).

## Configuration (appsettings.json)

You must provide a `SelectedDatabase` entry and the corresponding connection string name expected by the provider.

Example:

```json
{
  "SelectedDatabase": "SqlServer",
  "ConnectionStrings": {
    "SqlServerConnection": "Server=.;Database=DotnetBaseKit;Trusted_Connection=True;"
  }
}
```

Provider → expected connection string key mapping:

- `MySql` → `MysqlConnection`
- `SqlServer` → `SqlServerConnection`
- `Postgres` → `PostgresConnection`
- `SqLite` → `SqliteConnection`

If the required connection string is missing, the extension will throw an `InvalidOperationException`.

## How to register

In your `Program.cs` register your concrete `TContext` (which must inherit `BaseContext`) like:

```csharp
builder.Services.AddDbContext<YourConcreteContext>(builder.Configuration);
```

The extension will configure EF Core for the selected provider and set the `MigrationsAssembly` to `typeof(TContext).Assembly.FullName`.

## What is registered

- EF Core `DbContext` configuration for the selected provider.
- Generic repository implementations:
  - `IBaseWriteRepository<>` → `BaseWriteRepository<>`
  - `IBaseReadRepository<>` → `BaseReadRepository<>`
- `BaseContext` is registered as scoped to the concrete `TContext`.

The helper method that performs this registration is `RepositoryExtensions.AddSqlRepository(IServiceCollection)` and is invoked by `AddDbContext<TContext>()`.

## Notes

- If `SelectedDatabase` is not one of the supported providers the extension throws `NotSupportedException` with the configured value.
- The extension uses `ServerVersion.AutoDetect(connectionString)` for MySQL to determine server compatibility.
- This package is focused on wiring and example repository implementations; adapt repository internals (Dapper, EF Core queries or raw ADO.NET) as needed for your project.

## Examples and links

If you want to inspect or follow the examples in the source code, here are the main entry points:

- `RepositoryExtensions` (provider selection and DI registration): [DotnetBaseKit.Components.Infra.Sql/RepositoryExtensions.cs](DotnetBaseKit.Components.Infra.Sql/RepositoryExtensions.cs#L1)
- `BaseContext` (base `DbContext` used by repositories): [DotnetBaseKit.Components.Infra.Sql/Context/Base/BaseContext.cs](DotnetBaseKit.Components.Infra.Sql/Context/Base/BaseContext.cs#L1)
- `BaseReadRepository<>` (generic read implementation): [DotnetBaseKit.Components.Infra.Sql/Repositories/Base/BaseReadRepository.cs](DotnetBaseKit.Components.Infra.Sql/Repositories/Base/BaseReadRepository.cs#L1)
- `BaseWriteRepository<>` (generic write implementation): [DotnetBaseKit.Components.Infra.Sql/Repositories/Base/BaseWriteRepository.cs](DotnetBaseKit.Components.Infra.Sql/Repositories/Base/BaseWriteRepository.cs#L1)
- Domain contract interfaces (e.g., `IBaseReadRepository<>`, `IBaseWriteRepository<>`): [DotnetBaseKit.Components.Domain.Sql/Repositories/IBaseReadRepository.cs](DotnetBaseKit.Components.Domain.Sql/Repositories/IBaseReadRepository.cs#L1) and [DotnetBaseKit.Components.Domain.Sql/Repositories/IBaseWriteRepository.cs](DotnetBaseKit.Components.Domain.Sql/Repositories/IBaseWriteRepository.cs#L1)

Quick usage example in `Program.cs`:

```csharp
// registers the concrete DbContext and the generic repositories according to SelectedDatabase
builder.Services.AddDbContext<MyAppContext>(builder.Configuration);

// you can now inject IBaseReadRepository<MyEntity> and IBaseWriteRepository<MyEntity>
```

These links point to the reference implementations in the repository — open them to see implementation details and associated tests.
