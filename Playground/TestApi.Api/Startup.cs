using Fraga.Components.Api.Responses;
using Fraga.Components.Application.Base;
using Fraga.Components.Application.Pagination;
using Fraga.Components.Shared.Notifications;
using TestApi.Infra.CrossCutting.IoC;

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
