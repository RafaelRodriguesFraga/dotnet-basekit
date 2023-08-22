
using DotnetBoilerplate.Components.Application.Base;
using DotnetBoilerplate.Components.Shared.Notifications;
using TestApi.Application.ViewModels;
using TestApi.Domain.Repositories;

namespace TestApi.Application.Services
{
    public class TestApiServiceApplication : BaseServiceApplication, ITestApiServiceApplication
    {
        private readonly ITestApiWriteRepository _testApiWriteRepository;
        public TestApiServiceApplication(NotificationContext notificationContext, ITestApiWriteRepository testApiWriteRepository) : base(notificationContext)
        {
            _testApiWriteRepository = testApiWriteRepository;
        }

        public async Task CreateAsync(TestApiViewModel viewModel)
        {
            await _testApiWriteRepository.InsertOneAsync(viewModel);
        }
    }
}
