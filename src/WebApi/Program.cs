using Serilog;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using Serilog.Events;

namespace WebApi
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
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("HostName", HostName)
                .Enrich.WithProperty("ApplicationContext", AppName)
                .WriteTo.Console()
                .WriteTo.Seq(Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341")
                .CreateLogger();

            try
            {
                Log.Information("----- Configuring host ({ApplicationContext})...", AppName);
                var host = CreateHostBuilder(args).Build();

                Log.Information("----- Starting host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "----- Program terminated unexpectedly ({ApplicationContext}): {ErrorMessage}", AppName, ex.Message);
                return 1;
            }
            finally
            {
                Log.Information("----- Host stopped ({ApplicationContext}).", AppName);
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var startupException = Environment.GetEnvironmentVariable("STARTUP_EXCEPTION");

            var builder = Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, loggerConfiguration) =>
                {
                    Log.Debug("----- Configuring logging...");

                    loggerConfiguration
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.WithProperty("HostName", HostName)
                        .Enrich.WithProperty("ApplicationContext", AppName)
                        .WriteTo.Seq(Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341");

                    ExceptionProbe.ThrowIf(startupException, "CreateHostBuilder.UseSerilog");

                    Log.Debug("----- Closing startup logger ({ApplicationContext})...", AppName);
                    Log.CloseAndFlush();
                },
                writeToProviders: true)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Log.Debug("----- Configuring WebHost defaults...");

                    webBuilder
                        .UseStartup<Startup>()
                        .CaptureStartupErrors(false);

                    ExceptionProbe.ThrowIf(startupException, "CreateHostBuilder.ConfigureWebHostDefaults");
                });

            ExceptionProbe.ThrowIf(startupException, "CreateHostBuilder");

            return builder;
        }
    }
}
