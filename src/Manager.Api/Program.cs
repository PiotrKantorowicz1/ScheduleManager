using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Manager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                        .AddJsonFile($"appsettings.{env}.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //logging.AddDebug();
                    //logging.AddConsole();
                })
                .UseIISIntegration()
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}
