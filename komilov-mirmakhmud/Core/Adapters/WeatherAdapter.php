

<?php
class WeatherAdapter
{
    public function getWeatherImpact()
    {
        $temperature = 25;
        if ($temperature > 30) return "High energy consumption expected.";
        else return "Normal energy usage.";
    }
}
