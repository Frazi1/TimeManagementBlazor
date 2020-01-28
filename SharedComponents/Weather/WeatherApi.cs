using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace SharedComponents.Weather
{
    public class WeatherApi: IWeatherApi
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;

        public WeatherApi(string apiKey, HttpClient client)
        {
            _apiKey = apiKey;
            _client = client;
        }

        public async Task<WeatherForecast> GetForecastAsync(string city)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["access_key"] = _apiKey;
            query["query"] = city;

            var builder = new UriBuilder("http://api.weatherstack.com/current") {Query = query.ToString()};


            var response = await _client.GetStringAsync(builder.ToString());
            JObject jObject = JObject.Parse(response);

            if (jObject.ContainsKey("error"))
            {
                string errorMessage = jObject["error"]["info"].Value<string>();
                return WeatherForecast.FromError(errorMessage);
            }
            
            string description = string.Join(", ", jObject["current"]["weather_descriptions"].Values<string>());
            string temp = jObject["current"]["temperature"].Value<string>();
            
            string actualCity = jObject["location"]["name"].Value<string>();
            string country = jObject["location"]["country"].Value<string>();
            string location = $"{actualCity}, {country}";
                              
            var result = WeatherForecast.FromSuccess(location, temp, description);
            return result;
        }
    }
}