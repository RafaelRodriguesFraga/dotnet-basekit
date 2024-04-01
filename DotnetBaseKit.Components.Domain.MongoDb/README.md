# DotnetBaseKit.Components.Domain.MongoDB Documentation

This documentation provides an overview of the purpose, and usage for the `DotnetBaseKit.Components.Domain.MongoDB` package.

<details open>
  
  <summary>Table of Contents</summary>

- [DotnetBaseKit.Components.Domain.MongoDB Documentation](#dotnetbasekitcomponentsdomainmongodb-documentation)
  - [Overview](#overview)
  - [Usage](#usage)
    - [BaseDto](#basedto)
    - [BaseEntity](#baseentity)
    - [BaseRepository](#baserepository)
  - [Conclusion](#conclusion)
  
</details>

## Overview

The `DotnetBaseKit.Components.Domain.MongoDB` package provides essential base classes for building .NET applications using MongoDB as a database. It includes base Dtos, base Entities, and base Repositories to expedite domain-driven development.

## Usage

First, you have to install this package via Nuget or .NET CLI. 

If you're using Package Manager Console: 

`Install-Package DotnetBaseKit.Components.Domain.MongoDb`

Or if you're using the .NET CLI: 

`dotnet add package DotnetBaseKit.Components.Domain.MongoDb`

Now, in your `Program.cs` file, add the dependency for the repository interfaces:

```csharp

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// other dependencies
builder.Services.AddMongoDb(configuration);
```

Or if you have a `Startup.cs` file, add in your Configure Services method: 

```csharp
// other dependencies

services.AddMongoDb(Configuration);
```

*If you having trouble, see the TestApi Playground Startup.cs for more details.*

Now, in your appsettings.json add the MongoDB settings

```json
"MongoSettings": {
   "DatabaseName": "your-database-name",
   "ConnectionString": "your-database-connection-string"
}, 
```

You are now able to use the package.

### BaseDto

To create a domain-specific Dto, inherit from BaseDto and implement the Validate() method to perform custom validation for the Dto.

```csharp

using DotnetBaseKit.Components.Domain.MongoDb.Dtos.Base;

public class TestDto : BaseDto
{
    public string TestString { get; set; }

    public override void Validate()
    {
        if(string.IsNullOrEmpty(TestString))
        {
            AddNotification("Error", "TestString is required.");
        }                        
    }
}
```
If you prefer (I do), you can call an Abstract Validator class on the Validate method

```csharp
public class TestDtoContract : AbstractValidator<TestDto>
{
    public TestDtoContract()
    {
        RuleFor(dto => dto.TestString)
            .NotEmpty()
            .WithMessage("Cannot be null or empty");
    }
}
```

```csharp
public class TestDto : BaseDto
{
    public string TestString { get; set; }

    public override void Validate()
    {
        var contract = new TestDtoContract();
        var validationResult = contract.Validate(this);

        AddNotifications(validationResult);                    
    }
}
```

### BaseEntity

To create a domain-specific Entity, inherit from BaseEntity and implement the Validate() method to perform a custom validation.

```csharp

using DotnetBaseKit.Components.Domain.MongoDb.Entities.Base;

namespace TestApi.Domain.Entities
{
    public class Test : BaseEntity
    {
        public Test(string testString)
        {
            TestString = testString;
        }

        public string TestString { get; private set; }

        public override void Validate()
        {
            // Validation like BaseDto above
        }
    }
}
```

### BaseRepository

To create domain-specific repository interfaces for MongoDB, inherit from IBaseWriteRepository<TEntity> or BaseReadRepository<TEntity> depending of your needs. Implement any specific repository methods and logic as needed. 

```csharp
public interface ITestApiWriteRepository : IBaseWriteRepository<Test>
{
    // other methods
}
```

```csharp
public interface ITestApiReadRepository : IBaseReadRepository<Test>
{
    // other methods
}
```

## Conclusion

The `DotnetBaseKit.Components.Domain.MongoDB` package provides a set of essential base classes and interfaces to streamline the development of .NET applications that use MongoDB as a database. This package is designed to expedite domain-driven development by offering foundational components for building data transfer objects (DTOs), domain entities, and MongoDB repositories.