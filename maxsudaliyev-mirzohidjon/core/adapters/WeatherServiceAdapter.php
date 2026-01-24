<?php
namespace core\adapters;

/**
 * PATTERN: Adapter
 * Purpose: Wrap ExternalWeatherApi and expose WeatherService interface.
 */
class WeatherServiceAdapter implements WeatherService
{
    private ExternalWeatherApi $api;

    public function __construct(ExternalWeatherApi $api)
    {
        $this->api = $api;
    }

    public function getCurrentWeather(): string
    {
        $data = $this->api->fetch();
        return $data["state"] . ", " . $data["temp_c"] . "°C";
    }
}
