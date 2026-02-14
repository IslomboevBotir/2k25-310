namespace SmartCitySystem.Core.Adapters;

/// <summary>
/// External legacy weather service with incompatible interface
/// </summary>
public class LegacyWeatherService
{
    public string GetTemperatureData()
    {
        return "25.5C";
    }

    public string GetHumidityData()
    {
        return "65%";
    }

    public string GetWindData()
    {
        return "15 km/h NW";
    }
}

/// <summary>
/// Target interface that our system expects
/// </summary>
public interface IWeatherProvider
{
    WeatherData GetWeatherData();
}

/// <summary>
/// Weather data structure
/// </summary>
public class WeatherData
{
    public double Temperature { get; set; }
    public int Humidity { get; set; }
    public string WindSpeed { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Temperature: {Temperature}°C, Humidity: {Humidity}%, Wind: {WindSpeed}";
    }
}

/// <summary>
/// Adapter Pattern: Adapts the legacy weather service to our interface
/// Allows integration of external/legacy systems with incompatible interfaces
/// </summary>
public class WeatherServiceAdapter : IWeatherProvider
{
    private readonly LegacyWeatherService _legacyService;

    public WeatherServiceAdapter(LegacyWeatherService legacyService)
    {
        _legacyService = legacyService;
    }

    public WeatherData GetWeatherData()
    {
        // Adapt the legacy service data to our expected format
        var tempStr = _legacyService.GetTemperatureData();
        var humidityStr = _legacyService.GetHumidityData();
        var windStr = _legacyService.GetWindData();

        return new WeatherData
        {
            Temperature = ParseTemperature(tempStr),
            Humidity = ParseHumidity(humidityStr),
            WindSpeed = windStr
        };
    }

    private double ParseTemperature(string tempStr)
    {
        // Parse "25.5C" format
        var numberPart = tempStr.Replace("C", "").Trim();
        return double.TryParse(numberPart, out var temp) ? temp : 0.0;
    }

    private int ParseHumidity(string humidityStr)
    {
        // Parse "65%" format
        var numberPart = humidityStr.Replace("%", "").Trim();
        return int.TryParse(numberPart, out var humidity) ? humidity : 0;
    }
}

/// <summary>
/// Another external traffic data service with different interface
/// </summary>
public class ExternalTrafficService
{
    public string GetTrafficStatus()
    {
        return "HEAVY|Main Street|15";
    }
}

/// <summary>
/// Target interface for traffic data
/// </summary>
public interface ITrafficDataProvider
{
    TrafficData GetTrafficData();
}

/// <summary>
/// Traffic data structure
/// </summary>
public class TrafficData
{
    public string Status { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int VehicleCount { get; set; }

    public override string ToString()
    {
        return $"Location: {Location}, Status: {Status}, Vehicles: {VehicleCount}";
    }
}

/// <summary>
/// Adapter for external traffic service
/// </summary>
public class TrafficServiceAdapter : ITrafficDataProvider
{
    private readonly ExternalTrafficService _externalService;

    public TrafficServiceAdapter(ExternalTrafficService externalService)
    {
        _externalService = externalService;
    }

    public TrafficData GetTrafficData()
    {
        // Parse the pipe-delimited format from external service
        var dataStr = _externalService.GetTrafficStatus();
        var parts = dataStr.Split('|');

        return new TrafficData
        {
            Status = parts.Length > 0 ? parts[0] : "UNKNOWN",
            Location = parts.Length > 1 ? parts[1] : "Unknown Location",
            VehicleCount = parts.Length > 2 && int.TryParse(parts[2], out var count) ? count : 0
        };
    }
}
