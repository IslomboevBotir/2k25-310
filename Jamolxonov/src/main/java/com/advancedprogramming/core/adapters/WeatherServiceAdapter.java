package com.advancedprogramming.core.adapters;

import com.advancedprogramming.external.ExternalWeatherService;
import com.advancedprogramming.external.ExternalWeatherService.WeatherData;
/**
 * ADAPTER PATTERN
 * Purpose: Convert incompatible external weather service interface
 * to format compatible with SmartCity system
 * Enables integration with third-party services without modifying a core system
 */
public class WeatherServiceAdapter {
    private ExternalWeatherService externalService;

    public WeatherServiceAdapter() {
        this.externalService = new ExternalWeatherService();
    }

    /**
     * Adapts external weather data to simple string format
     * SmartCity system expects simple conditions, but external service
     * provides a complex WeatherData object
     */
    public String getCurrentConditions() {
        WeatherData data = externalService.fetchWeatherData();

        // Adapt the complex external format to a simple internal format
        return formatWeatherReport(data);
    }

    /**
     * Additional method: Get detailed weather for energy optimization
     */
    public WeatherInfo getDetailedWeather() {
        WeatherData data = externalService.fetchWeatherData();

        // Convert an external format to an internal WeatherInfo format
        return new WeatherInfo(
                data.temperatureCelsius,
                data.humidityPercent,
                data.windSpeedKmh,
                data.conditions
        );
    }

    private String formatWeatherReport(WeatherData data) {
        return String.format("%s, %.1f°C, Humidity: %d%%, Wind: %.1f km/h",
                data.conditions,
                data.temperatureCelsius,
                data.humidityPercent,
                data.windSpeedKmh);
    }

    /**
     * Internal weather representation
     * Compatible with SmartCity system
     */
    public static class WeatherInfo {
        private double temperature;
        private int humidity;
        private double windSpeed;
        private String conditions;

        public WeatherInfo(double temperature, int humidity, double windSpeed, String conditions) {
            this.temperature = temperature;
            this.humidity = humidity;
            this.windSpeed = windSpeed;
            this.conditions = conditions;
        }

        public double getTemperature() { return temperature; }
        public int getHumidity() { return humidity; }
        public double getWindSpeed() { return windSpeed; }
        public String getConditions() { return conditions; }

        public boolean isEnergyEfficientConditions() {
            // Logic to determine if conditions are good for energy savings
            return temperature > 15 && temperature < 25 && windSpeed < 20;
        }
    }
}
