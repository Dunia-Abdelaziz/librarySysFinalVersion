using BLL.DTOs.weather;
using System.Net.Http.Json;

namespace BLL.Services.WeatherService
{
    // Implement the weather service
    public class OpenWeatherMapService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenWeatherMapService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<WeatherDTO> GetWeatherDataAsync(string cityName)
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                WeatherData weatherData = await response.Content.ReadFromJsonAsync<WeatherData>();

                if (weatherData != null)
                {
                    float temperatureKelvin = weatherData.Main.Temp;
                    float temperatureCelsius = temperatureKelvin - 273.15f;

                    return new WeatherDTO
                    {
                        CityName = weatherData.Name,
                        TemperatureCelsius = temperatureCelsius
                    };
                }
                else
                {
                    throw new InvalidOperationException("Unable to deserialize the JSON response.");
                }
            }
            else
            {
                throw new HttpRequestException($"Error: {response.StatusCode}");
            }
        }
    }
}
