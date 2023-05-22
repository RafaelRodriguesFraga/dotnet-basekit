using TestApi.Application.ViewModels;

namespace TestApi.Application.Services
{
    public interface ITestApiServiceApplication
    {
        Task CreateAsync(TestApiViewModel viewModel);
    }
}
