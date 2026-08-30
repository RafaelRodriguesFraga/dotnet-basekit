using DotnetBaseKit.Components.Domain.Sql.Repositories;
using DotnetBaseKit.Components.Infra.Sql;
using DotnetBaseKit.Components.Infra.Sql.Context.Base;
using DotnetBaseKit.Components.Infra.Sql.Repositories.Base;
using DotnetBaseKit.Components.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBaseKit.Components.Infra.Sql.Tests
{
    public class RepositoryExtensionsTests
    {
        [Fact(DisplayName = "Should throw ArgumentNullException when services is null")]
        public void Should_Throw_ArgumentNullException_When_Services_Is_Null()
        {
            var configuration = new ConfigurationBuilder().Build();

            Assert.Throws<ArgumentNullException>(() =>
                RepositoryExtensions.AddDbContext<FakeBaseContext>(null!, configuration));
        }

        [Fact(DisplayName = "Should throw ArgumentNullException when configuration is null")]
        public void Should_Throw_ArgumentNullException_When_Configuration_Is_Null()
        {
            var services = new ServiceCollection();

            Assert.Throws<ArgumentNullException>(() =>
                services.AddDbContext<FakeBaseContext>(null!));
        }

        [Fact(DisplayName = "Should throw NotSupportedException when selected database is not supported")]
        public void Should_Throw_NotSupportedException_When_Database_Is_Not_Supported()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["SelectedDatabase"] = "Oracle"
                })
                .Build();

            var exception = Assert.Throws<NotSupportedException>(() =>
                services.AddDbContext<FakeBaseContext>(configuration));

            Assert.Equal(
                "Selected database 'Oracle' is not supported.",
                exception.Message);
        }

        [Fact(DisplayName = "Should throw NotSupportedException when selected database is not configured")]
        public void Should_Throw_NotSupportedException_When_Database_Is_Not_Configured()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .Build();

            var exception = Assert.Throws<NotSupportedException>(() =>
                services.AddDbContext<FakeBaseContext>(configuration));

            Assert.Equal(
                "Selected database '(not configured)' is not supported.",
                exception.Message);
        }

        [Fact(DisplayName = "Should throw InvalidOperationException when SQLite connection string is not configured")]
        public void Should_Throw_InvalidOperationException_When_Sqlite_Connection_String_Is_Missing()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["SelectedDatabase"] = "SqLite"
                })
                .Build();

            var exception = Assert.Throws<InvalidOperationException>(() =>
                services.AddDbContext<FakeBaseContext>(configuration));

            Assert.Equal(
                "Connection string 'SqliteConnection' is not configured.",
                exception.Message);
        }

        [Fact(DisplayName = "Should add SQLite DbContext")]
        public void Should_Add_Sqlite_DbContext()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["SelectedDatabase"] = "SqLite",
                    ["ConnectionStrings:SqliteConnection"] = "Data Source=test.db"
                })
                .Build();

            services.AddDbContext<FakeBaseContext>(configuration);

            using var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<FakeBaseContext>();

            Assert.NotNull(context);
            Assert.IsType<FakeBaseContext>(context);

            var database = context.Database;

            Assert.Equal("Microsoft.EntityFrameworkCore.Sqlite", database.ProviderName);
        }

        [Fact(DisplayName = "Should add SQL Server DbContext")]
        public void Should_Add_SqlServer_DbContext()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["SelectedDatabase"] = "SqlServer",
                    ["ConnectionStrings:SqlServerConnection"] =
                        "Server=localhost;Database=test;User Id=test;Password=test;TrustServerCertificate=True"
                })
                .Build();

            services.AddDbContext<FakeBaseContext>(configuration);

            using var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<FakeBaseContext>();

            Assert.NotNull(context);
            Assert.Equal(
                "Microsoft.EntityFrameworkCore.SqlServer",
                context.Database.ProviderName);
        }

        [Fact(DisplayName = "Should add PostgreSQL DbContext")]
        public void Should_Add_Postgres_DbContext()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["SelectedDatabase"] = "Postgres",
                    ["ConnectionStrings:PostgresConnection"] =
                        "Host=localhost;Database=test;Username=test;Password=test"
                })
                .Build();

            services.AddDbContext<FakeBaseContext>(configuration);

            using var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<FakeBaseContext>();

            Assert.NotNull(context);
            Assert.Equal(
                "Npgsql.EntityFrameworkCore.PostgreSQL",
                context.Database.ProviderName);
        }

        [Fact(DisplayName = "Should add SQL repositories")]
        public void Should_Add_Sql_Repositories()
        {
            var services = new ServiceCollection();

            services.AddDbContext<FakeBaseContext>(options =>
                options.UseSqlite("Data Source=:memory:"));

            services.AddScoped<BaseContext, FakeBaseContext>();

            RepositoryExtensions.AddSqlRepository(services);

            using var serviceProvider = services.BuildServiceProvider();

            var writeRepository =
                serviceProvider.GetService<IBaseWriteRepository<FakeBaseEntitySql>>();

            var readRepository =
                serviceProvider.GetService<IBaseReadRepository<FakeBaseEntitySql>>();

            Assert.NotNull(writeRepository);
            Assert.NotNull(readRepository);

            Assert.IsType<BaseWriteRepository<FakeBaseEntitySql>>(writeRepository);
            Assert.IsType<BaseReadRepository<FakeBaseEntitySql>>(readRepository);
        }

        [Fact(DisplayName = "Should throw ArgumentNullException when adding SQL repositories with null services")]
        public void Should_Throw_ArgumentNullException_When_Adding_Repositories_With_Null_Services()
        {
            Assert.Throws<ArgumentNullException>(() =>
                RepositoryExtensions.AddSqlRepository(null!));
        }

        [Fact(DisplayName = "Should register BaseContext as the selected context")]
        public void Should_Register_BaseContext_As_Selected_Context()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["SelectedDatabase"] = "SqLite",
                    ["ConnectionStrings:SqliteConnection"] = "Data Source=test.db"
                })
                .Build();

            services.AddDbContext<FakeBaseContext>(configuration);

            using var serviceProvider = services.BuildServiceProvider();

            var baseContext = serviceProvider.GetRequiredService<BaseContext>();

            Assert.NotNull(baseContext);
            Assert.IsType<FakeBaseContext>(baseContext);
        }
    }
}