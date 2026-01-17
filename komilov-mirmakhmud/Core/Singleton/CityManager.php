<?php
class CityManager
{
    private static $instance = null;

    private function __construct()
    {
        echo "CityManager initialized.\n";
    }

    public static function getInstance()
    {
        if (self::$instance === null) {
            self::$instance = new CityManager();
        }
        return self::$instance;
    }

    public function getStatus()
    {
        return "City is operational.";
    }
}
