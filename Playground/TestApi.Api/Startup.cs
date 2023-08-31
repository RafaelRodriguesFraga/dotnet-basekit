using DotnetBoilerplate.Components.Application;
using DotnetBoilerplate.Components.Infra.Sql;
using DotnetBoilerplate.Components.Api;
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

            services.AddApi();
            services.AddApplication();
            services.AddRepository();
            services.AddServiceApplication();
            services.AddMongoDb(Configuration);

            services.AddDbContext<SqlContext>(Configuration);

           
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
