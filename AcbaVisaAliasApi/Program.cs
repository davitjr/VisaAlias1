using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace AcbaVisaAliasApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().CaptureStartupErrors(true)
                        .ConfigureAppConfiguration(config =>
                        {
                            config.AddJsonFile("appsettings.log.json");
                        })
                        .UseSerilog((hostingContext, loggerConfiguration) =>
                        {
                            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                        });
                });
        }
    }
}
