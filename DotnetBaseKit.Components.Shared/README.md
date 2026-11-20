# DotnetBaseKit.Components.Shared Documentation

Shared types, notifications and helper components used across the DotnetBaseKit packages. This library groups cross-cutting primitives such as `NotificationContext`, response/notification models and DTO helpers.

<details open>
  
  <summary>Table of Contents</summary>

- [DotnetBaseKit.Components.Shared Documentation](#dotnetbasekitcomponentsshared-documentation)
  - [Overview](#overview)
  - [Contents](#contents)
  - [Registration](#registration)
  - [Usage Examples](#usage-examples)

</details>

## Overview

`DotnetBaseKit.Components.Shared` centralizes common primitives consumed by `Api`, `Application`, `Domain.*` and `Infra.*` packages. It is purposely small and framework-agnostic.

## Contents

- `Notifications/` - `NotificationContext` and related types used to accumulate and expose domain/application errors.
- `Responses/` - common response shapes used by API controllers (`IResponse`, `ResponseFactory`).
- `DtoHelpers`/`Mappers` - lightweight mapping helpers or interfaces for mapping between domain and view models.

## Registration

Register shared services in your application startup:

```csharp
builder.Services.AddShared();
```

This will register `NotificationContext`, `IResponseFactory` and other primitives used across components.

## Usage Examples

Notification usage in an application service:

```csharp
public class MyService : BaseServiceApplication
{
	public MyService(NotificationContext notificationContext)
		: base(notificationContext)
	{
	}

	public void DoSomething()
	{
		NotificationContext.AddError("field", "message");
	}
}
```

Response creation in controllers uses the `IResponseFactory` provided by Shared and registered by `AddShared()`.

---

Keep shared primitives stable and minimal to maximize reuse across packages.
