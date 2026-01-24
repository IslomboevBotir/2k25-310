<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Adapter;

interface ModernGridInterface
{
    public function connect(): void;
    public function optimizeUsage(): void;
}