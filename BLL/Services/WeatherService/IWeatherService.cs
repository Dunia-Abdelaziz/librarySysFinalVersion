using BLL.DTOs.weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.WeatherService
{
    public interface IWeatherService
    {
        Task<WeatherDTO> GetWeatherDataAsync(string cityName);
    }
}
