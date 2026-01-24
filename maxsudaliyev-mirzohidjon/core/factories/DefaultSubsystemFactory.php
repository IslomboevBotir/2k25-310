<?php
namespace core\factories;

use modules\lighting\LightingSystem;
use modules\transport\TransportSystem;
use modules\security\SecuritySystem;
use modules\energy\EnergySystem;
use core\adapters\WeatherService;
use core\adapters\ExternalWeatherApi;
use core\adapters\WeatherServiceAdapter;

/**
 * Abstract Factory implementation.
 */
class DefaultSubsystemFactory implements SubsystemFactory
{
    public function createLighting(): LightingSystem { return new LightingSystem(); }
    public function createTransport(): TransportSystem { return new TransportSystem(); }
    public function createSecurity(): SecuritySystem { return new SecuritySystem(); }
    public function createEnergy(): EnergySystem { return new EnergySystem(); }

    public function createWeatherService(): WeatherService
    {
        /**
         * PATTERN: Adapter
         * ExternalWeatherApi is a "foreign" API; adapter converts it to WeatherService interface.
         */
        return new WeatherServiceAdapter(new ExternalWeatherApi());
    }
}
