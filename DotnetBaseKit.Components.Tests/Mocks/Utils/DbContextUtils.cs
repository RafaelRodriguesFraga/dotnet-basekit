using Microsoft.EntityFrameworkCore;

namespace DotnetBaseKit.Components.Tests.Mocks.Utils
{
    public class DbContextUtils
    {
        public static FakeBaseContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<FakeBaseContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            var context = new FakeBaseContext(options);

            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            return context;
        }
    }
}