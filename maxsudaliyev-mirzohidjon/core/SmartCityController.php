<?php
namespace core;

use core\factories\SubsystemFactory;
use core\builders\CityReportBuilder;
use core\adapters\WeatherService;
use core\proxy\SecurityProxy;
use core\decorators\EnergySavingLightingDecorator;
use modules\lighting\LightingSystem;
use modules\transport\TransportSystem;
use modules\security\SecuritySystem;
use modules\energy\EnergySystem;

/**
 * PATTERNS:
 * 1) Singleton: SmartCityController is a single central instance (unique controller).
 * 2) Facade: Provides a simple interface to multiple subsystems (lighting/transport/security/energy).
 */
class SmartCityController
{
    private static ?SmartCityController $instance = null;

    private string $role = 'user'; // user|admin

    private LightingSystem $lighting;
    private TransportSystem $transport;
    private SecurityProxy $security; // Proxy wraps SecuritySystem
    private EnergySystem $energy;

    private WeatherService $weather;

    private function __construct(SubsystemFactory $factory)
    {
        $this->lighting  = $factory->createLighting();
        $this->transport = $factory->createTransport();
        $securityReal    = $factory->createSecurity();
        $this->security  = new SecurityProxy($securityReal, fn() => $this->role);

        $this->energy    = $factory->createEnergy();

        $this->weather   = $factory->createWeatherService();
    }

    public static function getInstance(SubsystemFactory $factory): SmartCityController
    {
        if (self::$instance === null) {
            self::$instance = new SmartCityController($factory);
        }
        return self::$instance;
    }

    // -------- Auth / Role ----------
    public function loginAs(string $role): void
    {
        $role = strtolower(trim($role));
        $this->role = ($role === 'admin') ? 'admin' : 'user';
    }

    public function getRole(): string
    {
        return $this->role;
    }

    // -------- Facade Methods ----------
    public function lightingStatus(): string { return $this->lighting->status(); }
    public function transportStatus(): string { return $this->transport->status(); }
    public function securityStatus(): string { return $this->security->status(); }
    public function energyStatus(): string { return $this->energy->status(); }

    public function addLightingZoneDemo(): void
    {
        // Demo data if first run
        $this->lighting->seedDemo();
    }

    public function toggleZone(string $zoneName, bool $on): string
    {
        return $this->lighting->toggleZone($zoneName, $on);
    }

    public function setLightBrightness(string $zoneName, string $lightName, int $value): string
    {
        return $this->lighting->setLightBrightness($zoneName, $lightName, $value);
    }

    public function enableEnergySavingMode(bool $enabled): string
    {
        /**
         * PATTERN: Decorator
         * We wrap LightingSystem with EnergySavingLightingDecorator to modify behavior
         * without changing original LightingSystem class.
         */
        if ($enabled) {
            $this->lighting = new EnergySavingLightingDecorator($this->lighting);
            return "Energy Saving Mode: ON (Lighting decorated)\n";
        }
        return "Energy Saving Mode: OFF (note: demo keeps current instance)\n";
    }

    public function addBusRoute(string $routeName): string
    {
        return $this->transport->addRoute($routeName);
    }

    public function dispatchBus(string $routeName): string
    {
        return $this->transport->dispatchBus($routeName);
    }

    public function armSecurity(): string { return $this->security->arm(); }
    public function disarmSecurity(): string { return $this->security->disarm(); }
    public function viewCameras(): string { return $this->security->viewCameras(); }

    public function optimizeEnergy(): string
    {
        $w = $this->weather->getCurrentWeather();
        return $this->energy->optimize($w);
    }

    public function buildCityReport(): string
    {
        /**
         * PATTERN: Builder
         * Build a structured city report step-by-step.
         */
        $builder = new CityReportBuilder();
        $report = $builder
            ->addHeader("SmartCity Report")
            ->addLine("Role: " . $this->role)
            ->addLine("--- Subsystems ---")
            ->addLine("Lighting: " . trim($this->lighting->status()))
            ->addLine("Transport: " . trim($this->transport->status()))
            ->addLine("Security: " . trim($this->security->status()))
            ->addLine("Energy: " . trim($this->energy->status()))
            ->addLine("--- Weather (Adapter) ---")
            ->addLine("Weather: " . $this->weather->getCurrentWeather())
            ->build();

        return (string)$report;
    }
}
