<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Factory;

class WeatherSensor implements SensorInterface
{
    public function readData(): string
    {
        return "Weather: 24°C, Clear Sky";
    }
}