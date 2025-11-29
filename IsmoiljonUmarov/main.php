<?php
declare(strict_types=1);

// Custom Autoloader
spl_autoload_register(function ($class) {
    $prefix = 'SmartCity\\';
    $base_dir = __DIR__ . '/src/';
    $len = strlen($prefix);
    if (strncmp($prefix, $class, $len) !== 0) return;
    $relative_class = substr($class, $len);
    $file = $base_dir . str_replace('\\', '/', $relative_class) . '.php';
    if (file_exists($file)) require $file;
});

use SmartCity\Core\CityFacade;

// Main Execution Loop
echo "========================================\n";
echo "   🏙️  SMART CITY SYSTEM (PHP 8.4)    \n";
echo "========================================\n";

try {
    $app = new CityFacade();
    $running = true;

    while ($running) {
        echo "\n[MENU]\n";
        echo "1. Start Morning Routine\n";
        echo "2. Trigger Fire Emergency\n";
        echo "3. Trigger Riot Emergency\n";
        echo "4. Attempt System Shutdown (Proxy Test)\n";
        echo "5. Run Unit Tests\n";
        echo "0. Exit\n";
        echo "Select option: ";

        $handle = fopen("php://stdin", "r");
        $choice = trim(fgets($handle));

        switch ($choice) {
            case '1':
                $app->startDay();
                break;
            case '2':
                $app->handleEmergency('fire');
                break;
            case '3':
                $app->handleEmergency('riot');
                break;
            case '4':
                echo "Enter Admin Password: ";
                $pass = trim(fgets($handle));
                $app->shutdownCity($pass);
                break;
            case '5':
                include 'tests/UnitTests.php';
                break;
            case '0':
                $running = false;
                echo "Goodbye.\n";
                break;
            default:
                echo "Invalid selection.\n";
        }
    }
} catch (\Throwable $e) {
    echo "CRITICAL ERROR: " . $e->getMessage() . "\n";
}