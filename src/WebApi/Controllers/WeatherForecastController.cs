using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IConfiguration configuration,
            IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _configuration = configuration;
            _applicationLifetime = applicationLifetime;
        }

        public int? ErrorCode { get; set; }

        [HttpGet]
        public async Task<ActionResult<WeatherForecastPage>> Get(
            [FromQuery]int? statusCode, 
            [FromQuery]int? delay, 
            [FromQuery]bool? exception)
        {
            try
            {
                if (delay.HasValue) await Task.Delay(delay.Value);
                if (exception.HasValue && exception.Value) throw new Exception("Exception requested");
                if (statusCode.HasValue) return StatusCode(statusCode.Value);

                var rng = new Random();
                var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

                var host = _configuration["HOSTNAME"] ?? _configuration["COMPUTERNAME"];

                return new WeatherForecastPage
                {
                    Host = host,
                    Items = forecast
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
