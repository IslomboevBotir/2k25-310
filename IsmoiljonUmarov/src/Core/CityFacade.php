<?php

declare(strict_types=1);

namespace SmartCity\Core;

use SmartCity\Patterns\Factory\SensorFactory;
use SmartCity\Patterns\Adapter\LegacyGridAdapter;
use SmartCity\Patterns\Adapter\LegacyCoalPlant;
use SmartCity\Patterns\Builder\EmergencyBuilder;
use SmartCity\Patterns\Proxy\SecurityProxy;

class CityFacade
{
    private CentralHub $hub;
    private SecurityProxy $securityProxy;

    public function __construct()
    {
        $this->hub = CentralHub::getInstance();
        $this->securityProxy = new SecurityProxy("admin");
    }

    public function startDay(): void
    {
        $this->hub->log('SYSTEM', 'Initializing Smart City Morning Routine...');

        // Read Sensors
        $traffic = SensorFactory::create('traffic');
        $weather = SensorFactory::create('weather');
        $this->hub->log('SENSOR', $traffic->readData());
        $this->hub->log('SENSOR', $weather->readData());

        //Optimize Energy
        $legacyPlant = new LegacyCoalPlant();
        $gridAdapter = new LegacyGridAdapter($legacyPlant);
        $gridAdapter->connect();
        $gridAdapter->optimizeUsage();
    }

    public function handleEmergency(string $type): void
    {
        $this->hub->log('ALERT', "Emergency reported: " . strtoupper($type));

        // Construct Response
        $builder = new EmergencyBuilder();

        $plan = match (strtolower($type)) {
            'fire' => $builder->addFireTruck()->addAmbulance()->setPriority('HIGH')->build(),
            'riot' => $builder->addPolice()->addSwatTeam()->setPriority('CRITICAL')->build(),
            default => $builder->addPolice()->setPriority('NORMAL')->build(),
        };

        echo $plan->execute();
    }

    public function shutdownCity(string $password): void
    {
        // Secure Access
        if ($this->securityProxy->grantAccess($password)) {
            $this->hub->log('SYSTEM', 'CRITICAL: CITY SHUTDOWN INITIATED.');
        } else {
            $this->hub->log('SECURITY', 'Unauthorized shutdown attempt blocked.');
        }
    }
}