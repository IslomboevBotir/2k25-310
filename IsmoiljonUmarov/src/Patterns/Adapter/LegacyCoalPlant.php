<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Adapter;

class LegacyCoalPlant
{
    public function fireUpBoilers(): void
    {
        echo "[Legacy System] Boilers firing up...\n";
    }

    public function setFuelRate(int $rate): void
    {
        echo "[Legacy System] Fuel rate set to $rate%.\n";
    }
}