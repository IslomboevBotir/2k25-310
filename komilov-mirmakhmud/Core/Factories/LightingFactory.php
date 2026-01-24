<?php
require_once __DIR__ . '/../../modules/Lighting/Light.php';

class LightingFactory
{
    public function createLight($type)
    {
        switch ($type) {
            case 'LED':
                return new Light('LED', 100);
            case 'Halogen':
                return new Light('Halogen', 150);
            default:
                return new Light('Standard', 80);
        }
    }
}
