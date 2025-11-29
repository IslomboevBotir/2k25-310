<?php
declare(strict_types=1);

use SmartCity\Core\CentralHub;
use SmartCity\Patterns\Factory\SensorFactory;
use SmartCity\Patterns\Builder\EmergencyBuilder;
use SmartCity\Patterns\Proxy\SecurityProxy;

function test(bool $condition, string $name): void
{
    echo $condition ? "✅ PASS: $name\n" : "❌ FAIL: $name\n";
}

echo "\n--- RUNNING UNIT TESTS ---\n";

// 1. Singleton Test
$hub1 = CentralHub::getInstance();
$hub2 = CentralHub::getInstance();
test($hub1 === $hub2, "Singleton returns same instance");

// 2. Factory Test
$sensor = SensorFactory::create('traffic');
test($sensor instanceof \SmartCity\Patterns\Factory\TrafficSensor, "Factory creates TrafficSensor");

// 3. Builder Test
$builder = new EmergencyBuilder();
$plan = $builder->addPolice()->setPriority('HIGH')->build();
test(in_array("Police Patrol", $plan->units), "Builder adds correct units");

// 4. Proxy Test
$proxy = new SecurityProxy('admin');
test($proxy->grantAccess('root123') === true, "Proxy allows correct password");
test($proxy->grantAccess('wrong') === false, "Proxy blocks wrong password");

echo "--------------------------\nTests Completed.\n";