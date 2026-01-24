<?php
namespace core\factories;

use modules\lighting\LightingSystem;
use modules\transport\TransportSystem;
use modules\security\SecuritySystem;
use modules\energy\EnergySystem;
use core\adapters\WeatherService;

/**
 * PATTERN: Abstract Factory
 * Purpose: Create related subsystem objects consistently (lighting/transport/security/energy + integrations).
 */
interface SubsystemFactory
{
    public function createLighting(): LightingSystem;
    public function createTransport(): TransportSystem;
    public function createSecurity(): SecuritySystem;
    public function createEnergy(): EnergySystem;
    public function createWeatherService(): WeatherService;
}
