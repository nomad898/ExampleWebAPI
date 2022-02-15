using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Example.Business.Core.Interfaces;
using Example.Business.Core.Services;
using Example.Database.EF.Context.Configurations;

namespace Example.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(options => 
            {
                var version = "v1";
                options.SwaggerDoc(version, new OpenApiInfo()
                {
                    Version = version,
                    Title = "Example",
                    Description = "Example App for testing purposes"
                });
            });

            services.AddDbContext<DatabaseContext>(options =>
            {
                var connectionStringName = "ExampleCS";
                options.UseSqlServer(Configuration.GetConnectionString(connectionStringName));
            });
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => 
                {
                    var version = "v1";
                    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
