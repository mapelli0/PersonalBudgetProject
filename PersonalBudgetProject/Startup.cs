using Autofac;
using BaseCleanArchitectureProject.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BaseCleanArchitectureProject
{
    public class Startup
    {

		private readonly IWebHostEnvironment _env;

		public IConfiguration Configuration { get; }

		public Startup(IConfiguration config, IWebHostEnvironment env) {
			Configuration = config;
			_env = env;
		}


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext(Configuration);

			services.AddControllers().AddNewtonsoftJson();

			services.AddSwaggerGen(c => {
										c.SwaggerDoc("v1",
													new OpenApiInfo {
																			Title = "BaseCleanArchitectureProject API",
																			Version = "v1"
																	});
										c.EnableAnnotations();
									});

		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseCleanArchitectureProject"));

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("Hello World!");
                });
				endpoints.MapDefaultControllerRoute();
            });
        }

		public void ConfigureContainer (ContainerBuilder builder) {
			builder.RegisterModule(new DefaultInfrastructureModule(_env.EnvironmentName == "Development"));
		}
    }
}
