// Core/Adapters/Adapter.cs

namespace SmartCitySystem.Core.Adapters
{
    // SmartCity tizimi kutgan interfeys (Target)
    public interface ICityWeather
    {
        double GetCurrentTemperatureC();
        bool IsRaining();
    }

    // Tashqi tizim (Adaptee) - Biz o'zgartira olmaydigan sinf
    public class ThirdPartyWeatherService
    {
        public int FetchTemperatureKelvin() => 295;
        public string GetPrecipitationStatus() => "yomg'ir_yo'q";
    }

    // Adapter sinfi: Tashqi tizimni ichki interfeysga moslashtiradi
    public class ThirdPartyWeatherAdapter : ICityWeather
    {
        private readonly ThirdPartyWeatherService _adaptee;

        public ThirdPartyWeatherAdapter(ThirdPartyWeatherService adaptee)
        {
            // Adapter Tashqi tizimning nusxasini ushlab turadi
            _adaptee = adaptee;
        }

        public double GetCurrentTemperatureC()
        {
            // Kelvindan Selsiyga konvertatsiya mantiqi
            return _adaptee.FetchTemperatureKelvin() - 273.15;
        }

        public bool IsRaining()
        {
            // String formatidan bool ga konvertatsiya mantiqi
            return _adaptee.GetPrecipitationStatus().ToLower().Contains("yomg'ir_bor");
        }
    }
}