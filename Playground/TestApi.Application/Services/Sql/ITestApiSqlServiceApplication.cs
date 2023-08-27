using TestApi.Application.ViewModels;

namespace TestApi.Application.Services
{
    public interface ITestApiSqlServiceApplication
    {
        Task CreateAsync(TestApiViewModel viewModel);
        Task UpdateAsync(Guid id, TestApiViewModel viewModel);
        Task DeleteAsync(Guid id);
    }
}
