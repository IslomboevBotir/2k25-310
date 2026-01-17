<?php
require_once __DIR__ . '/../Singleton/CityManager.php';
require_once __DIR__ . '/../Factories/LightingFactory.php';
require_once __DIR__ . '/../Builders/TransportBuilder.php';
require_once __DIR__ . '/../Adapters/WeatherAdapter.php';
require_once __DIR__ . '/../Proxy/SecurityProxy.php';
require_once __DIR__ . '/../../modules/energy/EnergyMeter.php';


class Controller
{
    private static $instance = null;
    private $cityManager;

    private function __construct()
    {
        $this->cityManager = CityManager::getInstance();
    }

    public static function getInstance()
    {
        if (self::$instance === null) {
            self::$instance = new Controller();
        }
        return self::$instance;
    }

    public function manageLighting()
    {
        $factory = new LightingFactory();
        $light = $factory->createLight('LED');
        echo $light->status() . "\n";
    }

    public function manageTransport()
    {
        $builder = new TransportBuilder();
        $vehicle = $builder->setType('Bus')->setCapacity(50)->build();
        echo $vehicle->status() . "\n";
    }

    public function manageSecurity()
    {
        $proxy = new SecurityProxy();
        $proxy->accessControl('admin');
    }

    public function manageEnergy()
    {
        $adapter = new WeatherAdapter();
        $weatherImpact = $adapter->getWeatherImpact();

        $energyMeter = new EnergyMeter();
        $energyMeter->adjustConsumption($weatherImpact);

        echo $energyMeter->status() . "\n";
        echo "Weather impact: $weatherImpact\n";
    }
}
