
namespace Adapters
{
    public class ExternalWeatherApi
    {
        public string FetchWeatherReport()
        {
            return "Sunny, 25°C, light breeze";
        }
    }

    public class ExternalWeatherAdapter : IWeatherService
    {
        private ExternalWeatherApi _api = new ExternalWeatherApi();

        public string GetCurrentWeather()
        {
            return _api.FetchWeatherReport();
        }
    }
}
