using System.Threading.Tasks;

namespace SharedComponents.Weather
{
    public interface IWeatherApi
    {
        Task<WeatherForecast> GetForecastAsync(string city);
    }
}