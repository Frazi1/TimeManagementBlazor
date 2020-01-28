using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedComponents.Weather;

namespace TimeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherApi _weatherApi;

        public WeatherController(IWeatherApi weatherApi)
        {
            _weatherApi = weatherApi;
        }
        
        [HttpGet]
        public async Task<WeatherForecast> Query([FromQuery(Name = "city")] string city)
        {
            return await _weatherApi.GetForecastAsync(city);
        }
    }
}