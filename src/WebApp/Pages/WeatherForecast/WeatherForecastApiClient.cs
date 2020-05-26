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

        public string GetApiUri => $"{client.BaseAddress}{ApiGetUrl}";

        public async Task<HttpResponseMessage> GetForecastAsync(string query = null)
        {
            var builder = new UriBuilder(GetApiUri);
            builder.Query = query;

            return await client.GetAsync(builder.ToString());
        }
    }
}