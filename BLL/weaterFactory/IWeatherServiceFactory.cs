using BLL.Services.WeatherService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.weaterFactory
{
    // Define a factory interface for the weather service
    public interface IWeatherServiceFactory
    {
        IWeatherService CreateWeatherService();
    }
}
