<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Factory;

interface SensorInterface
{
    public function readData(): string;
}