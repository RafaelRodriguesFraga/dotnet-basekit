# DotnetBaseKit.Components.Domain.Sql Documentation

This package provides domain-layer contracts and SQL related helpers intended to be implemented by infrastructure projects. It contains repository interfaces, DTOs and shared domain types useful for SQL-based persistence.

<details open>
  
	<summary>Table of Contents</summary>

- [Overview](#overview)
- [Purpose](#purpose)
- [Repository Contracts](#repository-contracts)
- [Usage](#usage)
- [Example](#example)

</details>

## Overview

`DotnetBaseKit.Components.Domain.Sql` holds abstractions and types that represent SQL persistence concerns at the Domain layer. It is framework-agnostic and does not depend on any specific ORM or database provider.

## Purpose

- Provide repository interfaces for read/write operations against relational stores.
- Define DTOs and helper types used across repository implementations.
- Keep domain contracts separated from infrastructure details.

## Repository Contracts

Common interfaces you may find here include:

- `IReadRepository<TEntity>` - read operations (Find, FindAll, FindAllPaginated).
- `IWriteRepository<TEntity>` - write operations (Insert, Update, Delete).

Implement these interfaces in your infra project (for example in `DotnetBaseKit.Components.Infra.Sql`).

## Usage

This package is consumed by Application services. A typical flow:

1. Define domain entity in `Domain.Sql.Entities` or in the domain project.
2. Implement repository interfaces in an infra project (SQL provider).
3. Register implementations in DI in `Program.cs` of the application.

## Example

Repository interface example:

```csharp
public interface ITestReadRepository : IReadRepository<TestEntity>
{
		Task<(IEnumerable<TestEntity> result, int totalRecords)> FindAllPaginatedAsync(
				int page, int quantityPerPage, Expression<Func<TestEntity, bool>> filterExpression);
}
```

Implement this contract in your SQL infrastructure and return domain entities to the Application layer.

---

Keep this package focused on contracts and domain DTOs; implementation details belong to infra packages.
