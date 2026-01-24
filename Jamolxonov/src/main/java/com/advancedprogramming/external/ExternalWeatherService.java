package com.advancedprogramming.external;

/**
 * External third-party weather service
 * Has incompatible interface with our system
 */
public class ExternalWeatherService {

    public WeatherData fetchWeatherData() {
        // Simulating external API call
        return new WeatherData(22.5, 65, 15.5, "Partly Cloudy");
    }

    public static class WeatherData {
        public double temperatureCelsius;
        public int humidityPercent;
        public double windSpeedKmh;
        public String conditions;

        public WeatherData(double temp, int humidity, double wind, String conditions) {
            this.temperatureCelsius = temp;
            this.humidityPercent = humidity;
            this.windSpeedKmh = wind;
            this.conditions = conditions;
        }
    }
}
