using System.Collections.Generic;

namespace WebApi
{
    public class WeatherForecastPage
    {
        public string Host { get; set; }
        
        public WeatherForecast[] Items { get; set; }
    }
}