namespace SmartCitySystem.Adapters
{
    public interface IWeather { string GetCurrentWeather(); }

    public class ExternalWeatherService
    {
        public string GetWeatherAsString() => "Quyoshli, +25°C";
    }

    public class WeatherAdapter : IWeather
    {
        private readonly ExternalWeatherService _service;
        public WeatherAdapter(ExternalWeatherService s) => _service = s;
        public string GetCurrentWeather() => _service.GetWeatherAsString();
    }
}