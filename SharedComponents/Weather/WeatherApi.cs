using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace SharedComponents.wwwroot.Weather
{
    public class WeatherApi
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

            var builder = new UriBuilder("http://api.weatherstack.com/") {Query = query.ToString()};


            var response = await _client.GetStringAsync(builder.ToString());
            JObject jObject = JObject.Parse(response);
        }
    }
}