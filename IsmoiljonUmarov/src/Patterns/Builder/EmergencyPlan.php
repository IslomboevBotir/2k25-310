<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Builder;

class EmergencyPlan
{
    public array $units = [];
    public string $priority = 'NORMAL';

    public function execute(): string
    {
        return sprintf(
            "\n>>> ACTION PLAN EXECUTED <<<\n[Priority]: %s\n[Dispatched]: %s\n",
            $this->priority,
            implode(', ', $this->units)
        );
    }
}