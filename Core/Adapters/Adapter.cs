namespace SmartCitySystem.Core.Adapters
{
    public interface ICityWeather
    {
        double GetCurrentTemperatureC();
        bool IsRaining();
    }

    public class ThirdPartyWeatherService
    {
        public int FetchTemperatureKelvin() => 295;
        public string GetPrecipitationStatus() => "yomg'ir_yo'q";
    }

    public class ThirdPartyWeatherAdapter : ICityWeather
    {
        private readonly ThirdPartyWeatherService _adaptee;

        public ThirdPartyWeatherAdapter(ThirdPartyWeatherService adaptee)
        {
            _adaptee = adaptee;
        }

        public double GetCurrentTemperatureC()
        {
            return _adaptee.FetchTemperatureKelvin() - 273.15;
        }

        public bool IsRaining()
        {
            return _adaptee.GetPrecipitationStatus().ToLower().Contains("yomg'ir_bor");
        }
    }
}