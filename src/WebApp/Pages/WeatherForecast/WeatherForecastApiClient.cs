using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Pages.WeatherForecast
{
    public class WeatherForecastApiClient
    {
        private const string ApiGetUrl = "WeatherForecast";
        private readonly HttpClient client;

        public WeatherForecastApiClient(HttpClient client)
        {
            this.client = client;
        }

        public string ApiGetUri => $"{client.BaseAddress}{ApiGetUrl}";

        public async Task<HttpResponseMessage> GetForecastAsync()
        {
            return await client.GetAsync(ApiGetUri);
        }
    }
}