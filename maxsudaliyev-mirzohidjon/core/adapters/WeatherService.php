<?php
namespace core\adapters;

/**
 * PATTERN: Adapter (Target interface)
 * Purpose: SmartCity uses this interface, no matter what external provider is used.
 */
interface WeatherService
{
    public function getCurrentWeather(): string;
}
