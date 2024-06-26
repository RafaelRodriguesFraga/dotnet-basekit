# DotnetBaseKit.Components.Application Documentation

This documentation provides an overview of its purpose, structure, and usage for the `DotnetBaseKit.Components.Application` package.

<details open>
  
  <summary>Table of Contents</summary>

- [DotnetBaseKit.Components.Application Documentation](#dotnetbasekitcomponentsapplication-documentation)
  - [Overview](#overview)
- [1. BaseServiceApplication](#1-baseserviceapplication)
  - [1.1. Class Overview](#11-class-overview)
  - [1.2. Constructor](#12-constructor)
    - [1.2.1. public BaseServiceApplication(NotificationContext notificationContext)](#121-public-baseserviceapplicationnotificationcontext-notificationcontext)
  - [1.3. Parameters](#13-parameters)
  - [1.4. Properties](#14-properties)
  - [1.5. Usage](#15-usage)
- [2. Pagination](#2-pagination)
  - [2.1. Class Overview](#21-class-overview)
  - [2.2. Constructors](#22-constructors)
    - [2.2.1. public PaginationResponse(int currentPage, int totalPages)](#221-public-paginationresponseint-currentpage-int-totalpages)
    - [2.2.2. public PaginationResponse(IEnumerable data, int currentPage, int totalPages, long totalRecords)](#222-public-paginationresponseienumerable-data-int-currentpage-int-totalpages-long-totalrecords)
  - [2.3. Properties](#23-properties)
  - [2.4. Usage](#24-usage)
  - [3. Conclusion](#3-conclusion)
  
</details>

## Overview

The `DotnetBaseKit.Components.Application` is part of the DotnetBaseKit architecture, designed to provide a foundational structure for various service applications. It is intended to be inherited by other service application classes to promote code reusability and maintain a consistent approach to handling notifications and related functionalities.

# 1. BaseServiceApplication

## 1.1. Class Overview

The BaseServiceApplication class resides within the `DotnetBaseKit.Components.Application.Base` namespace. It inherits the interface IBaseServiceApplication and is constructed to leverage the NotificationContext component.

## 1.2. Constructor

  ### 1.2.1. public BaseServiceApplication(NotificationContext notificationContext)

This constructor initializes an instance of the BaseServiceApplication class. It requires a NotificationContext parameter, which is used to manage and handle notifications within the service application.

## 1.3. Parameters

    notificationContext (Type: NotificationContext): An instance of the NotificationContext class, responsible for managing notifications within the service application.

## 1.4. Properties

The BaseServiceApplication class does not introduce any additional properties (for now) beyond those inherited from the IBaseServiceApplication interface. In later releases it might come to have.

## 1.5. Usage


The primary purpose of the BaseServiceApplication class is to serve as a base class for other service application components. By inheriting from this class, developers can take advantage of its notification handling capabilities and maintain a consistent approach to managing notifications across the application.

First, you have to install this package via Nuget or .NET CLI. 

If you're using Package Manager Console: 

`Install-Package DotnetBaseKit.Components.Application`

Or if you're using the .NET CLI: 

`dotnet add package DotnetBaseKit.Components.Application`

Now, in your `Program.cs` file, add the dependency that contains BaseServiceApplication interface and Pagination:

```csharp
// other dependencies

builder.Services.AddApplication();
```
Or if you have a `Startup.cs` file, add in your Configure Services method: 

```csharp
// other dependencies

services.AddApplication();
```

*If you having trouble, see the TestApi Playground Startup.cs for more details.*

With the dependencies added, you're ready to use it like below. You need to derive your service from `BaseServiceApplication`

```csharp  
   using DotnetBaseKit.Components.Application.Base;
   using DotnetBaseKit.Components.Shared.Notifications;

   namespace YourNamespace
   {
       public class YourServiceApplication : BaseServiceApplication, IYourServiceApplication
       {
         // interfaces
         
           public YourServiceApplication(NotificationContext notificationContext)
               : base(notificationContext)
           {
             // Initialize your interfaces
           }

            // implementation for IYourServiceApplication
       }
   }  
``` 

In practice, developers can create new service applications by inheriting from the BaseServiceApplication class and implementing the necessary business logic specific to their use case. The constructor ensures that each service application instance has access to a shared NotificationContext instance for handling notifications.

# 2. Pagination

## 2.1. Class Overview

The PaginationResponse class within the DotnetBaseKit.Components.Application.Pagination namespace is a component designed to handle pagination-related responses in the DotnetBaseKit application. It implements the IPaginationResponse<TData> interface and is designed to be a versatile response structure that contains pagination-related information along with a collection of data.

## 2.2. Constructors

### 2.2.1. public PaginationResponse(int currentPage, int totalPages)

This constructor initializes a new instance of the PaginationResponse class with the current page and total pages information.
Parameters

    currentPage (Type: int): The current page number.
    totalPages (Type: int): The total number of pages.

### 2.2.2. public PaginationResponse(IEnumerable<TData> data, int currentPage, int totalPages, long totalRecords)

This constructor initializes a new instance of the PaginationResponse class with a collection of data, current page details, total pages, and total record count.
Parameters

    data (Type: IEnumerable<TData>): A collection of data items.
    currentPage (Type: int): The current page number.
    totalPages (Type: int): The total number of pages.
    totalRecords (Type: long): The total number of records.

## 2.3. Properties

The PaginationResponse class exposes the following properties:

    Data (Type: IEnumerable<TData>): The collection of data items.
    CurrentPage (Type: int): The current page number.
    TotalPages (Type: int): The total number of pages.
    TotalRecords (Type: long): The total number of records.

## 2.4. Usage

The PaginationResponse class is used as a standardized response structure for paginated data. It encapsulates both the data and pagination-related information, making it easy to transmit and process paginated results.

Developers can use this class when implementing methods that return paginated data. By utilizing the PaginationResponse class, developers can ensure consistent and predictable responses that include the data collection, current page number, total pages, and total record count.

In this example a method was created in a Test Repository to get all records paginated in a MongoDB collection. Returns a tuple with the result and totalRecords.

```csharp  
  
   public async Task<(IEnumerable<TEntity> result, int totalRecords)> FindAllPaginatedAsync(
       int page, int quantityPerPage, Expression<Func<TEntity, bool>> filterExpression)
   {
       var skip = page == 1 ? 0 : (page - 1) * quantityPerPage;

       var collection = _collection.Find(filterExpression);

       var totalRecords = (int)collection.Count();

       var result = await collection
                .Skip(skip)
                .Limit(quantityPerPage)
                .SortByDescending(p => p.CreatedAt)
                .ToListAsync();

       return (result, totalRecords);
   }
  
``` 

Then, it was used in one of the ServiceApplications: 

```csharp  
   public async Task<PaginationResponse<Test>> GetAllAsync(
            int currentPage, int quantityPerPage, string something)
   {
       var (tests, totalRecords) = await _testReadRepository.FindAllPaginatedAsync(
                  currentPage, quantityPerPage, p => p.Something == something);

       return new PaginationResponse<Test>(currentPage, quantityPerPage, totalRecords, tests);
   }  
``` 

The result will look like this: 

```json
  {
      "currentPage": 1,
      "quantityPerPage": 10,
      "totalRecords": 2,
      "totalPages": 1,
      "data": [
         {
            "testData": "Test data 1"
         },
         {
            "testData": "Test data 2"
         },
      ],
      "success": true,
      "errors": []
  }
```

## 3. Conclusion

The DotnetBaseKit application leverages two fundamental components, the `BaseServiceApplication` class and the `PaginationResponse` class, to enhance its architecture and streamline its data handling processes. These components contribute to a more organized and efficient approach to managing notifications and paginated data retrieval within the application.
