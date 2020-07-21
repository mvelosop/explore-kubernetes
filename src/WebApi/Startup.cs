using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebApi
{
    public class Startup
    {
        readonly string startupException;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            startupException = Configuration["STARTUP_EXCEPTION"];

            ExceptionProbe.ThrowIf(startupException, "Startup");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Debug("----- Begin configuring services.");

            services.AddControllers();

            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddApplicationInsightsKubernetesEnricher();

            ExceptionProbe.ThrowIf(startupException, "ConfigureServices");

            Log.Debug("----- End configuring services.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Log.Debug("----- Begin configuring pipeline.");

            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrWhiteSpace(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.MapWhen(context =>
                context.Request.Path.Value.StartsWith("/hc/"),
                ab => ab.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("OK.");
                })
            );

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ExceptionProbe.ThrowIf(startupException, "Configure");

            Log.Debug("----- End configuring pipeline.");
        }
    }
}
