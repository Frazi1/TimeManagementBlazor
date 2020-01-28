using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SharedComponents.Weather;

namespace TimeManagementClient
{
    public class WeatherApi: IWeatherApi
    {
        private readonly HttpClient _httpClient;

        public WeatherApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherForecast> GetForecastAsync(string city)
        {
            return await _httpClient.GetJsonAsync<WeatherForecast>($"weather?city={city}");
        }
    }
}