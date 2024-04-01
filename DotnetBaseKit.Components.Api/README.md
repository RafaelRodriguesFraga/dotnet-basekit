# DotnetBaseKit.Components.Api Documentation

<details open>
  
  <summary>Table of Contents</summary>

- [DotnetBaseKit.Components.Api Documentation](#dotnetbasekitcomponentsapi-documentation)
  - [Overview](#overview)
  - [Usage](#usage)
  - [Constructors](#constructors)
  - [Methods](#methods)
    - [`CreateResponse()`](#createresponse)
    - [`ResponseOk<TData>(TData result)`](#responseoktdatatdata-result)
    - [`ResponseOk<TData>(IEnumerable<TData> result)`](#responseoktdataienumerabletdata-result)
    - [`ResponseOk<TData>(PaginationResponse<TData> searchResult)`](#responseoktdatapaginationresponsetdata-searchresult)
    - [`ResponseCreated<TData>(TData result)`](#responsecreatedtdatatdata-result)
    - [`ResponseCreated()`](#responsecreated)
    - [`ResponseBadRequest<TData>(TData result)`](#responsebadrequesttdatatdata-result)
  - [Fully Usage Example](#fully-usage-example)
  - [Inspiration](#inspiration)
  - [Conclusion](#conclusion)

## Overview

The `DotnetBaseKit.Components.Api` package provides common functionality for WebApi Applications. It has `ApiControllerBase` that extends the `ControllerBase` class from Microsoft.AspNetCore.Mvc and encapsulates common response creation and handling logic.

## Usage

To use the `DotnetBaseKit.Components.Api`, first you need to import the package and derive your controller from `ApiControllerBase`

```csharp     
   
    using DotnetBaseKit.Components.Api;

    namespace YourNamespace
    {
        public class YourController : ApiControllerBase
        {
            public YourController(IResponseFactory responseFactory)
                : base(responseFactory)
            {
                // Initialize your controller
            }

            // Define your API endpoints
        }
    }
  
``` 

## Constructors

The `ApiControllerBase` class provides a single constructor that accepts an `IResponseFactory` parameter. This constructor is used to inject an instance of the response factory, which is responsible for creating API responses.

 `protected ApiControllerBase(IResponseFactory responseFactory)` 

## Methods

### `CreateResponse()`

`protected IActionResult CreateResponse()` 

This method creates a basic API response using the injected response factory. It returns an `IActionResult` representing the HTTP response. If the response's `Success` property is `false`, a BadRequest response is returned. Otherwise, an Ok response is returned with the response object.

### `ResponseOk<TData>(TData result)`

`protected IActionResult ResponseOk<TData>(TData result) where TData : class` 

This method creates an API response with a single data object of type `TData`. It returns an `IActionResult` representing the HTTP response. If the response's `Success` property is `false`, a BadRequest response is returned. Otherwise, an Ok response is returned with the response object.

### `ResponseOk<TData>(IEnumerable<TData> result)`
  
`protected IActionResult ResponseOk<TData>(IEnumerable<TData> result) where TData : class` 

This method creates an API response with a collection of data objects of type `TData`. It returns an `IActionResult` representing the HTTP response. If the response's `Success` property is `false`, a BadRequest response is returned. Otherwise, an Ok response is returned with the response object.

### `ResponseOk<TData>(PaginationResponse<TData> searchResult)`

`protected IActionResult ResponseOk<TData>(PaginationResponse<TData> searchResult) where TData : class` 

This method creates an API response with a pagination result of type `PaginationResponse<TData>`. It returns an `IActionResult` representing the HTTP response. If the response's `Success` property is `false`, a BadRequest response is returned. Otherwise, an Ok response is returned with the response object paginated.

### `ResponseCreated<TData>(TData result)`

`protected IActionResult ResponseCreated<TData>(TData result) where TData : class` 

This method creates an API response for a successful resource creation operation. It returns an `IActionResult` representing the HTTP response. If the response's `Success` property is `false`, a BadRequest response is returned. Otherwise, a Created response (HTTP 201) is returned with the response object.

### `ResponseCreated()`
  
`protected IActionResult ResponseCreated()` 

This method creates a basic API response for a successful resource creation operation. It returns an `IActionResult` representing the HTTP response. If the response's `Success` property is `false`, a BadRequest response is returned. Otherwise, a Created response (HTTP 201) is returned with the response object.

### `ResponseBadRequest<TData>(TData result)`

`protected IActionResult ResponseBadRequest<TData>(TData result) where TData : class` 

This method creates a BadRequest response with the provided data object. It returns an `IActionResult` representing the HTTP Response BadRequest (400).
  
## Fully Usage Example
First, you have to install this package via Nuget or .NET CLI. 

If you're using Package Manager Console: 

`Install-Package DotnetBaseKit.Components.Api`

Or if you're using the .NET CLI: 

`dotnet add package DotnetBaseKit.Components.Api`

Now, in your `Program.cs` file, add the dependency that contains Responses and Notifications:

```csharp
// other dependencies

builder.Services.AddApi();
```
Or if you have a `Startup.cs` file, add in your Configure Services method: 

```csharp
// other dependencies

services.AddApi();
```

*If you having trouble, see the TestApi Playground Startup.cs for more details.*

With the dependencies added, you're ready to use it like below

 ```csharp  
  
     using DotnetBaseKit.Components.Api.Base;
     using DotnetBaseKit.Components.Api.Responses;
     using Microsoft.AspNetCore.Mvc;
     using TestApi.Application.Services;
     using TestApi.Application.ViewModels;

     namespace TestApi.Api.Controllers
     {
         [Route("api/[controller]")]
         public class TestApiController : ApiControllerBase
         {
            private readonly ITestApiServiceApplication _testApiServiceApplication;
            public TestApiController(
                IResponseFactory responseFactory, 
                ITestApiServiceApplication testApiServiceApplication) 
                : base(responseFactory)
            {
                _testApiServiceApplication = testApiServiceApplication;
            }

            [HttpPost]
            public async Task<IActionResult> InsertAsync(TestApiViewModel viewModel)
            {
                await _testApiServiceApplication.CreateAsync(viewModel);

                return ResponseCreated();
            }
         }
     }  
  
```
<br> 
In this example, a TestService is called from the Application Layer. It accepts a `TestApiViewModel` object as a parameter, which represents the data to be inserted. It asynchronously calls the CreateAsync method of the `ITestApiServiceApplication` to perform the insertion. Upon successful, it returns a Created response using the ResponseCreated method previously mentioned .   

## Inspiration

This package was based on [Optsol.Components.Service](https://www.nuget.org/packages/Optsol.Components.Service). The whole set of components ( `DotnetBaseKit.Components.Api`, `DotnetBaseKit.Components.Application`, `DotnetBaseKit.Components.Domain.MongoDb`,  `DotnetBaseKit.Components.Domain.Sql`, `DotnetBaseKit.Components.Infra.MongoDb`, `DotnetBaseKit.Components.Infra.Sql`, `DotnetBaseKit.Components.Shared` ) is a bolierplate that was developed to make things a little bit easier for some people (and for learning purposes too) 

## Conclusion

The `DotnetBaseKit.Components.Api` package provides a set of methods that simplify the creation and handling of API responses in the WebApi Applications. By deriving your API controllers from `ApiControllerBase`, you can take advantage of the common response creation logic and ensure consistent error handling throughout your API.
