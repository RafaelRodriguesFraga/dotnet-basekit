
using DotnetBaseKit.Components.Application.Base;
using DotnetBaseKit.Components.Shared.Notifications;
using TestApi.Application.ViewModels;
using TestApi.Domain.Repositories;

namespace TestApi.Application.Services
{
    public class TestApiServiceApplication : BaseServiceApplication, ITestApiServiceApplication
    {
        private readonly ITestApiWriteRepository _testApiWriteRepository;
       
        public TestApiServiceApplication(NotificationContext notificationContext, ITestApiWriteRepository testApiSqlWriteRepository) : base(notificationContext)
        {
            _testApiWriteRepository = testApiSqlWriteRepository;
        }

        public async Task CreateAsync(TestApiViewModel viewModel)
        {
            await _testApiWriteRepository.InsertOneAsync(viewModel);
        }
    }
}
