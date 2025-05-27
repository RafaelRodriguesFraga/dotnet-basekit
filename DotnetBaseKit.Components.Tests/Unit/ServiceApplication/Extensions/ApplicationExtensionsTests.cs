// using DotnetBaseKit.Components.Application;
// using DotnetBaseKit.Components.Application.Base;
// using DotnetBaseKit.Components.Application.Pagination;
// using Microsoft.Extensions.DependencyInjection;

// namespace DotnetBaseKit.Components.Tests.ServiceApplication.Extensions;

// public class ApplicationExtensionsTests
// {
//     [Fact(DisplayName = "Should register IBaseServiceApplication and IPaginationResponse in DI")]
//     public void Should_Register_Services_In_DI()
//     {
//         var services = new ServiceCollection();

//         services.AddApplication();

//         var provider = services.BuildServiceProvider();

//         var baseService = provider.GetService<IBaseServiceApplication>();
//         Assert.NotNull(baseService);
//         Assert.IsType<BaseServiceApplication>(baseService);

//         var paginationService = provider.GetService<IPaginationResponse<string>>();
//         Assert.Null(paginationService);
//     }
// }