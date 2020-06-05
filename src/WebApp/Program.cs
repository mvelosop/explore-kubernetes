using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace WebApp
{
    public class Program
    {
        public static readonly string AppName = 
            Path.GetFileNameWithoutExtension(typeof(Program).Assembly.Location);

        public static readonly string HostName = 
            Environment.GetEnvironmentVariable("HOSTNAME") ?? 
            Environment.GetEnvironmentVariable("COMPUTERNAME");

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("HostName", HostName)
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(
                    Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341")
                .CreateLogger();

            try
            {
                Log.Information("Configuring host ({ApplicationContext})...", AppName);
                var host = CreateHostBuilder(args).Build();

                Log.Information("Starting host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
