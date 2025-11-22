<?php
require_once __DIR__ . '/src/Core/Facade/Controller.php';

$controller = Controller::getInstance();

while (true) {
    echo "\nWelcome to SmartCity System\n";
    echo "1. Manage Lighting\n";
    echo "2. Manage Transport\n";
    echo "3. Security & Surveillance\n";
    echo "4. Energy Monitoring\n";
    echo "0. Exit\n";
    echo "Select option: ";

    $option = trim(fgets(STDIN));

    switch ($option) {
        case '1':
            $controller->manageLighting();
            break;
        case '2':
            $controller->manageTransport();
            break;
        case '3':
            $controller->manageSecurity();
            break;
        case '4':
            $controller->manageEnergy();
            break;
        case '0':
            exit("Exiting SmartCity System...\n");
        default:
            echo "Invalid option. Try again.\n";
    }
}
