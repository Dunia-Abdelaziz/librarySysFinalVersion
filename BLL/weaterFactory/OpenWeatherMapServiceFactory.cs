using BLL.Services.WeatherService;

namespace BLL.weaterFactory
{
    // Implement the weather service factory
    public class OpenWeatherMapServiceFactory : IWeatherServiceFactory
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenWeatherMapServiceFactory(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public IWeatherService CreateWeatherService()
        {
            return new OpenWeatherMapService(_httpClient, _apiKey);
        }
    }
}
