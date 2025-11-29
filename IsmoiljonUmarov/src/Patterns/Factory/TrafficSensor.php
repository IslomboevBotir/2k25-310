<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Factory;

class TrafficSensor implements SensorInterface
{
    public function readData(): string
    {
        return "Traffic: Congestion Level 45%";
    }
}