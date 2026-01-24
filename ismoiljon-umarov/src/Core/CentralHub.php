<?php

declare(strict_types=1);

namespace SmartCity\Core;

class CentralHub
{
    private static ?CentralHub $instance = null;
    private array $logs = [];

    private function __construct() {}

    public static function getInstance(): CentralHub
    {
        if (self::$instance === null) {
            self::$instance = new CentralHub();
        }
        return self::$instance;
    }

    public function log(string $module, string $message): void
    {
        $timestamp = date('H:i:s');
        $entry = "[$timestamp] [$module] $message";
        $this->logs[] = $entry;
        echo $entry . PHP_EOL;
    }

//    public function getLogs(): array
//    {
//        return $this->logs;
//    }
}