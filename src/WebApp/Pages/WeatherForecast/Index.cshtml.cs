using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApp.Pages.WeatherForecast
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly WeatherForecastApiClient _client;
        private readonly IConfiguration _configuration;

        public IndexModel(
        ILogger<IndexModel> logger,
        WeatherForecastApiClient client,
        IConfiguration configuration)
        {
            _logger = logger;
            _client = client;
            _configuration = configuration;

            Host = _configuration["HOSTNAME"] ?? _configuration["COMPUTERNAME"];

        }

        public string GetApiUrl => _client.ApiGetUri;

        public WeatherForecastPage ForecastPage { get; set; }

        public string Host { get; set; }

        public string ErrorMessage { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int? statusCode { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? delay { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? exception { get; set; }

        public async Task OnGet()
        {
            try
            {
                var response = await _client.GetForecastAsync();
                var content = await response.Content.ReadAsStringAsync();
                ForecastPage = JsonConvert.DeserializeObject<WeatherForecastPage>(content);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
