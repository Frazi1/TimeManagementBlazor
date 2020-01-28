namespace SharedComponents.Weather
{
    public class WeatherForecast
    {
        public string City { get; set; }
        public string Temperature { get; set; }
        public string Description { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsSuccess => ErrorMessage == null;
        
        public WeatherForecast()
        {
        }
        
        private WeatherForecast(string city, string temperature, string description, string error)
        {
            City = city;
            Temperature = temperature;
            Description = description;
            ErrorMessage = error;
        }

        public static WeatherForecast FromSuccess(string city, string temperature, string description) 
            => new WeatherForecast(city, temperature, description, null);
        public static WeatherForecast FromError(string error) 
            => new WeatherForecast(null, null, null, error);
    }
}