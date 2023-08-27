using TestApi.Application.ViewModels;
using TestApi.Domain.Entities;

namespace TestApi.Application.Services
{
    public interface ITestApiSqlServiceApplication
    {
        Task CreateAsync(TestApiViewModel viewModel);
        Task UpdateAsync(Guid id, TestApiViewModel viewModel);
        Task DeleteAsync(Guid id);
    }
}
