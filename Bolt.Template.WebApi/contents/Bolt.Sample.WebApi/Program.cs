using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Diagnostics;

namespace Bolt.Sample.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hc, sp, lc) =>
                    lc.ReadFrom.Configuration(hc.Configuration)
                        .ReadFrom.Services(sp)
                        .Enrich.WithProperty("env", hc.HostingEnvironment.EnvironmentName))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .CaptureStartupErrors(true)
                        .UseStartup<Startup>();
                });
    }
}
