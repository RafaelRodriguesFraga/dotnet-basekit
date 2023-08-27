using DotnetBoilerplate.Components.Api.Responses;
using DotnetBoilerplate.Components.Application.Base;
using DotnetBoilerplate.Components.Application.Pagination;
using DotnetBoilerplate.Components.Infra.Sql;
using DotnetBoilerplate.Components.Shared.Notifications;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Configuration;
using System.Configuration;
using TestApi.Infra.CrossCutting.IoC;
using TestApi.Infra.Sql.Context;

namespace TestApi.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddRepository();
            services.AddServiceApplication();
            services.AddMongoDb(Configuration);

           services.AddDbContext<SqlContext>(Configuration);

            services.AddScoped<NotificationContext>();
            services.AddScoped<IResponseFactory, ResponseFactory>();
            services.AddScoped(typeof(IPaginationResponse<>), typeof(PaginationResponse<>));
            services.AddScoped<IBaseServiceApplication, BaseServiceApplication>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {

            app.UseSwagger();
            app.UseSwaggerUI();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
