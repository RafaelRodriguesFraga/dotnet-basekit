
using DotnetBaseKit.Components.Application.Base;
using DotnetBaseKit.Components.Shared.Notifications;
using TestApi.Application.ViewModels;
using TestApi.Domain.Repositories;

namespace TestApi.Application.Services
{
    public class TestApiSqlServiceApplication : BaseServiceApplication, ITestApiSqlServiceApplication
    {
        private readonly ITestApiSqlWriteRepository _testApiSqlWriteRepository;
        private readonly ITestApiSqlReadRepository _testApiSqlReadRepository;
        public TestApiSqlServiceApplication(NotificationContext notificationContext, ITestApiSqlWriteRepository testApiSqlWriteRepository, ITestApiSqlReadRepository testApiSqlReadRepository) : base(notificationContext)
        {
            _testApiSqlWriteRepository = testApiSqlWriteRepository;
            _testApiSqlReadRepository = testApiSqlReadRepository;
        }

        public async Task CreateAsync(TestApiViewModel viewModel)
        {
            await _testApiSqlWriteRepository.InsertAsync(viewModel);
        }

        public async Task UpdateAsync(Guid id, TestApiViewModel viewModel)
        {
            var test = await _testApiSqlReadRepository.GetByIdAsync(id);
            if (test is null)
            {
                _notificationContext.AddNotification("Error", "Test not found");
            }

            test.Update(viewModel.TestString);

            await _testApiSqlWriteRepository.UpdateAsync(test);
        }

        public async Task DeleteAsync(Guid id)
        {
            var test = await _testApiSqlReadRepository.GetByIdAsync(id);
            if (test is null)
            {
                _notificationContext.AddNotification("Error", "Test not found");
            }

            await _testApiSqlWriteRepository.DeleteAsync(test);

        }      
    }
}
