<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Adapter;

use SmartCity\Core\CentralHub;

class LegacyGridAdapter implements ModernGridInterface
{
    public function __construct(
        private LegacyCoalPlant $legacySystem
    ) {}

    public function connect(): void
    {
        CentralHub::getInstance()->log('ENERGY', 'Adapting Legacy Plant to Modern Grid...');
        $this->legacySystem->fireUpBoilers();
    }

    public function optimizeUsage(): void
    {
        CentralHub::getInstance()->log('ENERGY', 'Optimizing fuel consumption for Eco Standards...');
        $this->legacySystem->setFuelRate(40);
    }
}