using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using BaseCleanArchitectureProject.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BaseCleanArchitectureProject
{
    public class Program
    {

		public static async Task Main (string[] args) {
			try {
				var host = CreateHostBuilder(args).Build();

				//await host.InitialiseAsync(CancellationToken.None);

				await host.SeedAsync(CancellationToken.None);

				await host.RunAsync(CancellationToken.None);
			} catch (Exception exception) {
				throw;
			}
		}





        //public static void Main(string[] args) {
        //    CreateHostBuilder(args).Build().Run();
        //}

		public static IHostBuilder CreateHostBuilder (string[] args) {
			return Host.CreateDefaultBuilder(args)
						.UseServiceProviderFactory(new AutofacServiceProviderFactory())
						.ConfigureWebHostDefaults(webBuilder => {
													webBuilder.UseStartup<Startup>()
															.ConfigureLogging(logging => {
																				logging.ClearProviders();
																				logging.AddConsole();
																				// logging.AddAzureWebAppDiagnostics(); add this if deploying to Azure
																			});
												});
		}
    }
}
