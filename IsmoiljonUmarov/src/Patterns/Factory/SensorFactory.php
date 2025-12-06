<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Factory;

class SensorFactory
{
    public static function create(string $type): SensorInterface
    {
        return match (strtolower($type)) {
            'traffic' => new TrafficSensor(),
            'weather' => new WeatherSensor(),
            default => throw new \InvalidArgumentException("Unknown sensor type: $type"),
        };
    }
}