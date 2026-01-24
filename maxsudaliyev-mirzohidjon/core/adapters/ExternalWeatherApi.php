<?php
namespace core\adapters;

/**
 * Simulates an external / legacy service with incompatible interface.
 * (Imagine this is a real SDK.)
 */
class ExternalWeatherApi
{
    public function fetch(): array
    {
        $states = ["Sunny", "Cloudy", "Rainy", "Windy"];
        $temp = rand(-5, 40);
        return [
            "state" => $states[array_rand($states)],
            "temp_c" => $temp
        ];
    }
}
