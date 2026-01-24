<?php
require_once __DIR__ . '/core/autoload.php';

use core\factories\DefaultSubsystemFactory;
use core\SmartCityController;

function prompt(string $text): string {
    echo $text;
    return trim(fgets(STDIN));
}

$factory = new DefaultSubsystemFactory();
$controller = SmartCityController::getInstance($factory);
$controller->addLightingZoneDemo();

echo "============================\n";
echo " SmartCity System (Console)\n";
echo "============================\n\n";

while (true) {
    echo "\n--- MENU ---\n";
    echo "Role: " . $controller->getRole() . "\n";
    echo "1) Login as admin/user\n";
    echo "2) Lighting: status\n";
    echo "3) Lighting: toggle zone ON/OFF\n";
    echo "4) Lighting: set brightness (zone + light)\n";
    echo "5) Decorator: Energy Saving Mode ON\n";
    echo "6) Transport: add route\n";
    echo "7) Transport: dispatch bus\n";
    echo "8) Security: arm (admin only)\n";
    echo "9) Security: disarm (admin only)\n";
    echo "10) Security: view cameras\n";
    echo "11) Energy: optimize (uses Weather Adapter)\n";
    echo "12) Build city report (Builder)\n";
    echo "0) Exit\n";

    $choice = prompt("Choose: ");

    switch ($choice) {
        case "1":
            $r = prompt("Enter role (admin/user): ");
            $controller->loginAs($r);
            echo "OK. Role set.\n";
            break;

        case "2":
            echo $controller->lightingStatus();
            break;

        case "3":
            $z = prompt("Zone name (Center/Park): ");
            $on = strtolower(prompt("ON? (y/n): ")) === 'y';
            echo $controller->toggleZone($z, $on);
            break;

        case "4":
            $z = prompt("Zone: ");
            $l = prompt("Light name (e.g., MainStreet-1, Park-2): ");
            $b = (int)prompt("Brightness (0..100): ");
            echo $controller->setLightBrightness($z, $l, $b);
            break;

        case "5":
            echo $controller->enableEnergySavingMode(true);
            break;

        case "6":
            $r = prompt("Route name: ");
            echo $controller->addBusRoute($r);
            break;

        case "7":
            $r = prompt("Route name: ");
            echo $controller->dispatchBus($r);
            break;

        case "8":
            echo $controller->armSecurity();
            break;

        case "9":
            echo $controller->disarmSecurity();
            break;

        case "10":
            echo $controller->viewCameras();
            break;

        case "11":
            echo $controller->optimizeEnergy();
            break;

        case "12":
            echo $controller->buildCityReport();
            break;

        case "0":
            echo "Bye!\n";
            exit(0);

        default:
            echo "Unknown option.\n";
    }
}
