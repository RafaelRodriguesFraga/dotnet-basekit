# DotnetBaseKit.Components.Infra.MongoDb Documentation

Infrastructure helpers and repository implementations for MongoDB. This package contains extensions to register MongoDB clients, repository implementations and configuration helpers used by applications that rely on MongoDB as the data store.

<details open>
  
	<summary>Table of Contents</summary>

- [Overview](#overview)
- [Configuration](#configuration)
- [Registration](#registration)
- [Repository Example](#repository-example)

</details>

## Overview

`DotnetBaseKit.Components.Infra.MongoDb` provides ready-to-use MongoDB wiring for the DotnetBaseKit playground and examples: connection bootstrap, collection mapping helpers and sample repository implementations.

## Configuration

Add a connection string and database name to your `appsettings.json`:

```json
"Mongo": {
	"ConnectionString": "mongodb://localhost:27017",
	"Database": "testdb"
}
```

## Registration

Register MongoDB services in the DI container (from `Program.cs`):

```csharp
builder.Services.AddMongoDb(Configuration);
```

This method configures the `IMongoClient` and registers repository implementations to the configured interfaces from the Domain layer.

## Repository Example

Typical repository implementation uses the registered `IMongoDatabase`:

```csharp
public class TestMongoRepository : ITestReadRepository
{
		private readonly IMongoCollection<TestEntity> _collection;

		public TestMongoRepository(IMongoDatabase database)
		{
				_collection = database.GetCollection<TestEntity>("tests");
		}

		// Implement read methods
}
```

---

This package is intended as an example and can be adapted for production needs (indexes, conventions, serialization settings, resilience policies).
